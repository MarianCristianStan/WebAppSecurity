using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class CartRepository : RepositoryBase<Cart>, ICartRepository
   {
      public CartRepository(RobotShopContext context) : base(context) { }
   }
}
