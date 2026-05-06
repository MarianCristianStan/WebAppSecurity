using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IProductCategoryService : IGenericServiceRepo<ProductCategory>
	{
		ProductCategory GetCategoryByName(string name);
      ProductCategory GetCategoryById(string categoryId);

   }
}
