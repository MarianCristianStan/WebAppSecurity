using System;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using RobotShop.Models;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class LuceneIndexService : ILuceneIndexService
	{
		private const string LuceneDirectory = "LuceneIndex";
		private readonly FSDirectory _directory;
		private readonly Analyzer _analyzer;

		public LuceneIndexService()
		{
			_directory = FSDirectory.Open(new DirectoryInfo(LuceneDirectory));
			_analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
		}


		public void IndexProductSpecification(string productId, byte[] pdfBytes)
		{
			using var writer = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer));

			string pdfText = ExtractTextFromPdf(pdfBytes);

			var doc = new Document
				{
					 new StringField("ProductId", productId, Field.Store.YES),	
					 new TextField("Content", pdfText, Field.Store.YES)
            };

			writer.UpdateDocument(new Term("ProductId", productId), doc);
			writer.Commit();
		}

		public void ReindexAllProducts(IEnumerable<Product> products, IPdfService pdfService)
		{
			using var writer = new IndexWriter(_directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer));
			writer.DeleteAll();
			writer.Dispose();
		    
			foreach (var product in products)
			{
				try
				{
					var pdfBytes = pdfService.GenerateProductSpecificationPdf(product.ProductId);
					IndexProductSpecification(product.ProductId, pdfBytes);
				}
				catch
				{
				//log
				}
					
			}
		}


		public SearchResult[] SearchSpecifications(string query, int maxResults = 10)
		{
			using var reader = DirectoryReader.Open(_directory);
			var searcher = new IndexSearcher(reader);

			var parser = new QueryParser(LuceneVersion.LUCENE_48, "Content", _analyzer);
			var luceneQuery = parser.Parse(query);

			var hits = searcher.Search(luceneQuery, maxResults).ScoreDocs;

			return hits.Select(hit =>
			{
				var foundDoc = searcher.Doc(hit.Doc);
				return new SearchResult
				{
					ProductId = foundDoc.Get("ProductId"),
					Score = hit.Score 
				};
			}).OrderByDescending(r => r.Score).ToArray(); 
		}


		private string ExtractTextFromPdf(byte[] pdfBytes)
		{
			using var memoryStream = new MemoryStream(pdfBytes);
			using var pdfReader = new PdfReader(memoryStream);
			using var pdfDocument = new PdfDocument(pdfReader);

			string extractedText = "";
			for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
			{
				extractedText += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
			}	
			return extractedText;
		}
	}
}
