using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobotShop.Models;
using RobotShop.Services.Interfaces;

namespace RobotShop.Controllers
{
	public class AdminController : Controller
	{
		private readonly IProductService _productService;
		private readonly ILuceneIndexService _luceneIndexService;
		private readonly IPdfService _pdfService;
		
		public AdminController(IProductService productService, ILuceneIndexService luceneIndexService, IPdfService pdfService)
		{
			_pdfService = pdfService;
			_productService = productService;
			_luceneIndexService = luceneIndexService;
		}
		
		public IActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult ReindexLucene()
		{
			var products = _productService.GetAll();
			_luceneIndexService.ReindexAllProducts(products, _pdfService);

			TempData["SuccessMessage"] = "Lucene index a fost refăcut.";
			return RedirectToAction("Index");
		}


	}
}
