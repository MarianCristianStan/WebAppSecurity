using Microsoft.AspNetCore.Mvc;
using RobotShop.Services.Interfaces;
using System.Threading.Tasks;
using System.Xml.Linq;
using RobotShop.Models;
using RobotShop.Services;

namespace RobotShop.Controllers
{
   public class HomeController : Controller
   {
      private readonly IUserService _userService;
      private readonly IProductService _productService;
      private readonly IProductCategoryService _productCategoryService;
      private readonly ITFIDFSearchService _tfidfSearchService;
      private readonly ILuceneIndexService _luceneIndexService;
		private readonly IPdfService _pdfService;

		public HomeController(IUserService userService, IProductService productService, IProductCategoryService productCategoryService, ITFIDFSearchService tfidfSearchService, ILuceneIndexService luceneIndexService, IPdfService pdfService)
      {
         _userService = userService;
         _productService = productService;
         _productCategoryService = productCategoryService;
         _tfidfSearchService = tfidfSearchService;
         _luceneIndexService = luceneIndexService;
			_pdfService = pdfService;

			var allProductTitles = _productService.GetAll().Select(p => p.Name).ToList();
         if (allProductTitles.Any())
         {
	         _tfidfSearchService.ComputeIDFScores(allProductTitles); 
         }

	
		}

      public async Task<IActionResult> Index(string category = null, string searchQuery = null)
      {
         var user = _userService.GetCurrentUser();
         ViewBag.IsAuthenticated = user != null;
         ViewBag.IsAdmin = user != null && await _userService.IsUserAdminAsync(user);

         List<Product> products = _productService.GetAll();
         List<ProductCategory> categories = _productCategoryService.GetAll();

         if (categories == null || !categories.Any())
         {
	         categories = new List<ProductCategory>();
         }
         ViewBag.Categories = categories;


         if (!string.IsNullOrEmpty(category))
         {
	         products = products.Where(p => p.ProductCategoryId.ToString() == category).ToList();
	         ViewBag.SelectedCategory = category;
         }

			if (!string.IsNullOrEmpty(searchQuery))
         {
	         products = products
		         .Select(p => new { Product = p, Score = _tfidfSearchService.ComputeTFIDF(p.Name, searchQuery) })
		         .Where(p => p.Score > 0) 
		         .OrderByDescending(p => p.Score)
		         .Select(p => p.Product)
		         .ToList();

	         ViewBag.searchQuery = searchQuery;
         }

			foreach (var product in products)
         {
	         product.ProductCategory = _productCategoryService.GetById(product.ProductCategoryId);
         }
         return View(products);
			
      }

		[HttpGet]
		public async Task<IActionResult> SearchSpecs(string specQuery, string sortOrder = "desc")
		{
			if (string.IsNullOrWhiteSpace(specQuery))
			{
				return RedirectToAction("Index");
			}

			var results = _luceneIndexService.SearchSpecifications(specQuery, maxResults: 20);

			if (sortOrder == "asc")
				results = results.OrderBy(r => r.Score).ToArray();
			else
				results = results.OrderByDescending(r => r.Score).ToArray();

			var matchingProducts = results
				.Select(r => _productService.GetById(r.ProductId))
				.Where(p => p != null)
				.ToList();

			foreach (var product in matchingProducts)
			{
				product.ProductCategory = _productCategoryService.GetById(product.ProductCategoryId);
			}

			var user = _userService.GetCurrentUser();
			ViewBag.IsAuthenticated = user != null;
			ViewBag.IsAdmin = user != null && await _userService.IsUserAdminAsync(user);

			ViewBag.Categories = _productCategoryService.GetAll(); 
			ViewBag.SelectedCategory = null; 
			ViewBag.searchQuery = null;

			ViewBag.IsSpecSearch = true;
			ViewBag.SpecResults = results;
			ViewBag.SpecQuery = specQuery;
			ViewBag.SortOrder = sortOrder;

			return View("Index", matchingProducts);
		}



		[HttpGet]
      public IActionResult GetSearchSuggestions(string term)
      {
         if (string.IsNullOrEmpty(term))
            return Json(new List<string>());

         var allProducts = _productService.GetAll().Select(p => p.Name).ToList();
         var suggestions = _tfidfSearchService.GetTopSuggestions(term, allProducts);

         return Json(suggestions);
      }

		
		public IActionResult Login()
      {
         return RedirectToPage("/Account/Login", new { area = "Identity" });
      }

      public IActionResult Register()
      {
         return RedirectToPage("/Account/Register", new { area = "Identity" });
      }

      

   }
}