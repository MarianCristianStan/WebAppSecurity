using RobotShop.Models;
using System.Collections.Generic;

namespace RobotShop.Services.Interfaces
{
	public interface ILuceneIndexService
	{
		void IndexProductSpecification(string productId, byte[] pdfBytes);
		SearchResult[] SearchSpecifications(string query, int maxResults = 10);
		void ReindexAllProducts(IEnumerable<Product> products, IPdfService pdfService);
	}

	public class SearchResult
	{
		public string ProductId { get; set; }
		public float Score { get; set; }
	}
}
