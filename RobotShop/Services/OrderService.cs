using Microsoft.EntityFrameworkCore;
using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class OrderService : GenericServiceRepo<Order>, IOrderService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IUserService _userService;
      private readonly IOrderProductService _orderProductService;
		public OrderService(IRepositoryWrapper repositoryWrapper, IUserService userService, IOrderProductService orderProductService)
			: base(repositoryWrapper.OrderRepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
			_userService = userService;	
         _orderProductService = orderProductService;
		}

      public List<Order> GetOrdersByUserId(string userId)
      {
         // Fetch orders and ensure products are included using the repository directly
         var orders = _repositoryWrapper.OrderRepository
            .FindByCondition(o => o.UserId == userId)
            .ToList();

         foreach (var order in orders)
         {
            order.OrderProducts = _repositoryWrapper.OrderProductRepository
               .FindByCondition(op => op.OrderId == order.OrderId)
               .ToList();
         }

         return orders;
      }

      public Order GetOrderById(string orderId)
      {
         // Fetch a single order and ensure products are included
         var order = _repositoryWrapper.OrderRepository
            .FindByCondition(o => o.OrderId == orderId)
            .FirstOrDefault();

         if (order != null)
         {
            order.OrderProducts = _repositoryWrapper.OrderProductRepository
               .FindByCondition(op => op.OrderId == orderId)
               .ToList();
         }
         return order;
      }


      public Order CreateOrder(Order order)
      {
         if (order == null || !order.OrderProducts.Any())
         {
            throw new InvalidOperationException("Cannot create an order with no products.");
         }
         Add(order);
         

         // Linking the correct OrderId to the OrderProducts
         foreach (var orderProduct in order.OrderProducts)
         {
            orderProduct.OrderId = order.OrderId; 
            _orderProductService.Update(orderProduct);

           
         }
         return order;
      }

		public Dictionary<Product, int> GetSoldProducts()
		{
			
			var orderProducts = _repositoryWrapper.OrderProductRepository.FindAll().ToList();
			var productIds = orderProducts.Select(op => op.ProductId).Distinct().ToList();
			var products = _repositoryWrapper.ProductRepository
				.FindByCondition(p => productIds.Contains(p.ProductId))
				.ToList()
				.ToDictionary(p => p.ProductId);

			return orderProducts
				.GroupBy(op => op.ProductId)
				.ToDictionary(
					g => products.ContainsKey(g.Key) ? products[g.Key] : null,
					g => g.Sum(op => op.Quantity)
				);
		}




	}
}
