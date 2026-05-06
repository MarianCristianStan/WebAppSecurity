using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IFeatureService : IGenericServiceRepo<Feature>
	{
		 Feature GetFeatureById(string featureId);
        IEnumerable<Feature> GetFeaturesByCategoryId(string categoryId);

   }
}
