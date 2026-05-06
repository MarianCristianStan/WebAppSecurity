using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IProductFeatureService : IGenericServiceRepo<ProductFeature>
	{
		List<ProductFeature> GetFeaturesByProductId(string productId);
		void DeleteCompositeKey(string productId, string featureId);
		public ProductFeature GetFeatureById(string productId, string featureId);
	}
}
