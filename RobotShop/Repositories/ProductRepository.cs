using Microsoft.EntityFrameworkCore;
using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class ProductRepository : RepositoryBase<Product>, IProductRepository
   {
      private readonly RobotShopContext _context;
      public ProductRepository(RobotShopContext context) : base(context)
      {
         _context = context;
      }
      public IQueryable<Product> FindByCondition(System.Linq.Expressions.Expression<System.Func<Product, bool>> expression)
      {
         return _context.Set<Product>().Where(expression).AsNoTracking();
      }
      public IEnumerable<Product> FindByCategory(string categoryId)
{
  
			string sql;

			if (string.IsNullOrEmpty(categoryId))
			{
				sql = "SELECT * FROM Products";
				return _context.Products.FromSqlRaw(sql).AsNoTracking().ToList();
			}
			else
			{
				sql = "SELECT * FROM Products WHERE ProductCategoryId = '" + categoryId + "'";
				return _context.Products.FromSqlRaw(sql, categoryId).AsNoTracking().ToList();
			}
		}
         }
}
