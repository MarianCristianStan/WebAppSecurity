using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface ICartProductService : IGenericServiceRepo<CartProduct>
	{
		List<CartProduct> GetCartProductsByCartId(string cartId);
		public void DeleteCompositeKey(string cartId, string productId);
	}
}
