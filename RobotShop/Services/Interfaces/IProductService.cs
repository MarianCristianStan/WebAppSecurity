using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IProductService : IGenericServiceRepo<Product>
	{
		IEnumerable<Product> GetProductsByCategory(string categoryId);
      IEnumerable<Product> SearchProducts(string searchTerm);



    }
}
