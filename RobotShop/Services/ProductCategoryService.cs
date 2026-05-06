using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class ProductCategoryService : GenericServiceRepo<ProductCategory>, IProductCategoryService
	{
		public ProductCategoryService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.ProductCategoryRepository, repositoryWrapper)
		{
		}

		public ProductCategory GetCategoryByName(string name)
		{
			return _repositoryWrapper.ProductCategoryRepository
				 .FindByCondition(c => c.Name.ToLower() == name.ToLower())
				 .FirstOrDefault();
		}

      public ProductCategory GetCategoryById(string categoryId)
      {
         return _repositoryWrapper.ProductCategoryRepository.FindByCondition(c => c.CategoryId == categoryId).FirstOrDefault();
      }
   }
}
