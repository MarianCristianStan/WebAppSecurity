using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class FeatureService : GenericServiceRepo<Feature>, IFeatureService
	{
		public FeatureService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.FeatureRepository, repositoryWrapper)
		{
		}
		public Feature GetFeatureById(string featureId)
		{
			return _repositoryWrapper.FeatureRepository.FindByCondition(f => f.FeatureId == featureId).FirstOrDefault();
		}
      public IEnumerable<Feature> GetFeaturesByCategoryId(string categoryId)
      {
         return _repositoryWrapper.FeatureRepository.FindByCondition(f => f.ProductCategoryId == categoryId).ToList();
      }
   }
}
