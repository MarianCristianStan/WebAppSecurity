using System.Collections.Generic;
using System.Linq;
using RobotShop.Models;
using RobotShop.Repositories.Interfaces;
using RobotShop.Services.Interfaces;

namespace RobotShop.Services
{
	public class UserAddressService : GenericServiceRepo<UserAddress>, IUserAddressService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public UserAddressService(IRepositoryWrapper repositoryWrapper)
			 : base(repositoryWrapper.UserAddressRepository, repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		public List<UserAddress> GetAddressesByUserId(string userId)
      {
         return _repositoryWrapper.UserAddressRepository
            .FindByCondition(ua => ua.UserId == userId)
            .ToList();
      }

      public UserAddress? GetFirstAddressByUserId(string userId)
      {
         return _repositoryWrapper.UserAddressRepository
            .FindByCondition(ua => ua.UserId == userId)
            .FirstOrDefault();
      }
   }
}
