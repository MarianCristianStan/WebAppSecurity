using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RobotShop.Repositories
{
   public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
   {
      protected RobotShopContext _RobotShopContext { get; set; }

      public RepositoryBase(RobotShopContext RobotShopContext)
      {
         this._RobotShopContext = RobotShopContext;
      }

      public IQueryable<T> FindAll()
      {
         return this._RobotShopContext.Set<T>().AsNoTracking();
      }

      public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
      {
         return this._RobotShopContext.Set<T>().Where(expression).AsNoTracking();
      }

      public void Create(T entity)
      {
         this._RobotShopContext.Set<T>().Add(entity);
      }

      public void Update(T entity)
      {
         this._RobotShopContext.Set<T>().Update(entity);
      }

      public void Delete(T entity)
      {
         this._RobotShopContext.Set<T>().Remove(entity);
      }
   }
}
