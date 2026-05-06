using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RobotShop.Models
{
   public class ProductFeature
   {
      [Required]
      [MaxLength(50)]
      public required string ProductId { get; set; }

      [ForeignKey("ProductId")]
      public Product? Product { get; set; }

      [Required]
      [MaxLength(50)]
      public required string FeatureId { get; set; }

      [ForeignKey("FeatureId")]
      public Feature? Feature { get; set; }

      [Required]
      [MaxLength(100)]
      public required string FeatureValue { get; set; }

   }
}
