using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class OrderRepository : RepositoryBase<Order>, IOrderRepository
   {
      public OrderRepository(RobotShopContext context) : base(context) { }
   }
}
