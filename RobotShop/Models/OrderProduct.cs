using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RobotShop.Models
{
   public class OrderProduct
   {
      [Required]
      [MaxLength(50)]
      public required string OrderId { get; set; }

      [ForeignKey("OrderId")]
      public Order? Order { get; set; }

      [Required]
      [MaxLength(50)]
      public required string ProductId { get; set; }

      [ForeignKey("ProductId")]
      public Product? Product { get; set; }

      [Required]
      [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
      public int Quantity { get; set; }
   }
}
