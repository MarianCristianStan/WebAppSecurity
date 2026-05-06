using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
   {
      public ProductCategoryRepository(RobotShopContext context) : base(context)
      {
      }
   }
}
