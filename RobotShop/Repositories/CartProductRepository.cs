using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class CartProductRepository : RepositoryBase<CartProduct>, ICartProductRepository
   {
      public CartProductRepository(RobotShopContext context) : base(context)
      {
      }
   }
}
