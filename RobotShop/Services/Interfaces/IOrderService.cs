using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IOrderService : IGenericServiceRepo<Order>
	{
		List<Order> GetOrdersByUserId(string userId);
      Order CreateOrder(Order order);
      Order GetOrderById(string orderId);
      Dictionary<Product, int> GetSoldProducts();

	}	
}
