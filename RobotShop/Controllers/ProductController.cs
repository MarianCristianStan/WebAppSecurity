using Microsoft.AspNetCore.Mvc;
using RobotShop.Services.Interfaces;
using RobotShop.Models;

public class ProductController : Controller
{
	private readonly IPdfService _pdfService;
	private readonly IProductService _productService;

	public ProductController(IPdfService pdfService, IProductService productService)
	{
		_pdfService = pdfService;
		_productService = productService;
	}

	public IActionResult DownloadProductSpecification(string productId)
	{
		var product = _productService.GetById(productId);

		if (product == null)
		{
			return NotFound("Product not found.");
		}

		var pdfBytes = _pdfService.GenerateProductSpecificationPdf(productId);

		var cleanProductName = string.Join("_", product.Name.Split(Path.GetInvalidFileNameChars()));
		var fileName = $"Specification_{cleanProductName}.pdf";

		return File(pdfBytes, "application/pdf", fileName);
	}
}