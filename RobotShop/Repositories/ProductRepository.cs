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
   }
}
