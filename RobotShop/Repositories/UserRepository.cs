using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class UserRepository : RepositoryBase<User>, IUserRepository
   {
      public UserRepository(RobotShopContext context) : base(context)
      {
      }
   }
}
