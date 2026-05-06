using Microsoft.EntityFrameworkCore;
using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class ProductFeatureService : GenericServiceRepo<ProductFeature>, IProductFeatureService
	{
		public ProductFeatureService(IRepositoryWrapper repositoryWrapper)
			: base(repositoryWrapper.ProductFeatureRepository, repositoryWrapper)
		{
		}

		public List<ProductFeature> GetFeaturesByProductId(string productId)
		{
			return _repositoryWrapper.ProductFeatureRepository.FindByCondition(f => f.ProductId == productId).ToList();
		}

		public void DeleteCompositeKey(string productId, string featureId)
		{
			var entity = _repository.FindByCondition(e =>
				EF.Property<string>(e, "FeatureId") == featureId &&
				EF.Property<string>(e, "ProductId") == productId
			).FirstOrDefault();

			if (entity != null)
			{
				_repository.Delete(entity);
				_repositoryWrapper.Save();
			}
		}

		public ProductFeature GetFeatureById(string productId, string featureId)
		{
			return _repositoryWrapper.ProductFeatureRepository
				.FindByCondition(f => f.ProductId == productId && f.FeatureId == featureId)
				.FirstOrDefault();
		}
	}
}
