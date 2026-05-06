using Microsoft.AspNetCore.Mvc;
using RobotShop.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RobotShop.Controllers
{
	[Route("Search")]
	public class SearchController : Controller
	{
		private readonly ILuceneIndexService _luceneIndexService;
		private readonly IProductService _productService;

		public SearchController(ILuceneIndexService luceneIndexService, IProductService productService)
		{
			_luceneIndexService = luceneIndexService;
			_productService = productService;
		}

		[HttpGet("SearchSpecifications")]
		public IActionResult SearchSpecifications(string query)
		{
			if (string.IsNullOrWhiteSpace(query))
				return Json(new { results = new List<object>() });

			var results = _luceneIndexService.SearchSpecifications(query, 10);

			var productResults = results.Select(result =>
			{
				var product = _productService.GetById(result.ProductId);
				return new
				{
					product.Name,
					product.Brand,
					product.Price,
					product.ProductId,
					Score = result.Score
				};
			}).OrderByDescending(p => p.Score).ToList();

			return Json(new { results = productResults });
		}
	}
}
