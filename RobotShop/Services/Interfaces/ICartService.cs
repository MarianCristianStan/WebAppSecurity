using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface ICartService : IGenericServiceRepo<Cart>
	{
		Cart GetCartByUserId(string userId);
      void AddToCart(string productId);
		void RemoveFromCart(string productId);
		void UpdateCartQuantity(string productId, int quantity);
		IEnumerable<CartProduct> GetCartItems();
      void ClearCart();
   }
}
