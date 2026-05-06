using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class OrderProductRepository : RepositoryBase<OrderProduct>, IOrderProductRepository
   {
      public OrderProductRepository(RobotShopContext context) : base(context)
      {
      }
   }
}
