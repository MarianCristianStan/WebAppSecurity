using RobotShop.Models;
using RobotShop.Repositories.Interfaces;

namespace RobotShop.Repositories
{
   public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
   {
      public UserAddressRepository(RobotShopContext context) : base(context)
      {
      }
   }
}
