using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class ProductFeatureRepository : RepositoryBase<ProductFeature>, IProductFeatureRepository
   {
      public ProductFeatureRepository(RobotShopContext context) : base(context) { }
   }
}
