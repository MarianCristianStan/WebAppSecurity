using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class FeatureRepository : RepositoryBase<Feature>, IFeatureRepository
   {
      public FeatureRepository(RobotShopContext context) : base(context) { }
   }
}
