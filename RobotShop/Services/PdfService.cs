using System.IO;
using System.Linq;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using RobotShop.Models;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
   public class PdfService : IPdfService
   {
      private readonly IProductService _productService;
      private readonly IProductFeatureService _productFeatureService;
      private readonly IFeatureService _featureService;
		private readonly ILuceneIndexService _luceneIndexService;
     

		public PdfService(IProductService productService, IProductFeatureService productFeatureService, IFeatureService featureService, ILuceneIndexService luceneIndexService)
      {
         _productService = productService;
         _productFeatureService = productFeatureService;
         _featureService = featureService;
			_luceneIndexService = luceneIndexService;
        
		}

      public byte[] GenerateProductSpecificationPdf(string productId)
      {
         var product = _productService.GetById(productId);
         if (product == null)
         {
            throw new ArgumentException("Product not found.");
         }

         var productFeatures = _productFeatureService.GetFeaturesByProductId(productId);
         var featureDetails = productFeatures
             .Select(pf => new
             {
                Name = _featureService.GetById(pf.FeatureId)?.Name ?? "Unknown Feature",
                Value = pf.FeatureValue
             })
             .ToList();

         using (MemoryStream memoryStream = new MemoryStream())
         {
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

           
            document.Add(new Paragraph(" Product Specification")
                .SetFont(titleFont)
                .SetFontSize(20)
                .SetFontColor(ColorConstants.BLUE)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(20));

          
            Table detailsTable = new Table(2).UseAllAvailableWidth();
            detailsTable.SetMarginBottom(15);

            detailsTable.AddCell(CreateStyledCell("Product Name:", titleFont, true));
            detailsTable.AddCell(CreateStyledCell(product.Name, normalFont));

            detailsTable.AddCell(CreateStyledCell("Brand:", titleFont, true));
            detailsTable.AddCell(CreateStyledCell(product.Brand, normalFont));

            detailsTable.AddCell(CreateStyledCell("Price:", titleFont, true));
            detailsTable.AddCell(CreateStyledCell($"${product.Price}", normalFont));

            document.Add(detailsTable);

           
            document.Add(new Paragraph(" Description")
                .SetFont(titleFont)
                .SetFontSize(14)
                .SetFontColor(ColorConstants.BLACK)
                .SetMarginBottom(5));

            document.Add(new Paragraph(product.Description)
                .SetFont(normalFont)
                .SetFontSize(12)
                .SetMarginBottom(15));

            
            document.Add(new Paragraph(" Features")
                .SetFont(titleFont)
                .SetFontSize(14)
            
                .SetFontColor(ColorConstants.BLACK)
                .SetMarginBottom(5));

            if (featureDetails.Any())
            {
               Table featureTable = new Table(2).UseAllAvailableWidth();
               featureTable.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
               featureTable.SetMarginBottom(20);

               featureTable.AddHeaderCell(CreateStyledCell("Feature", titleFont, true));
               featureTable.AddHeaderCell(CreateStyledCell("Value", titleFont, true));

               foreach (var feature in featureDetails)
               {
                  featureTable.AddCell(CreateStyledCell(feature.Name, normalFont));
                  featureTable.AddCell(CreateStyledCell(feature.Value, normalFont));
               }

               document.Add(featureTable);
            }
            else
            {
               document.Add(new Paragraph("No features available.")
                   .SetFont(normalFont)
                   .SetFontSize(12));
            }

            document.Close();
				byte[] pdfBytes = memoryStream.ToArray();
            /*_luceneIndexService.IndexProductSpecification(productId, pdfBytes);*/
            return pdfBytes;
			}
      }

      // Helper method to create styled table cells
      private Cell CreateStyledCell(string content, PdfFont font, bool isHeader = false)
      {
         Cell cell = new Cell()
             .Add(new Paragraph(content)
             .SetFont(font)
             .SetFontSize(12));

         if (isHeader)
         {
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
         }

         return cell;
      }
   }
}
