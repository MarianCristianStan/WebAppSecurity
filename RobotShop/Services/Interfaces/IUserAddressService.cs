using RobotShop.Models;
using System.Collections.Generic;

namespace RobotShop.Services.Interfaces
{
	public interface IUserAddressService : IGenericServiceRepo<UserAddress>
	{
		List<UserAddress> GetAddressesByUserId(string userId);
       UserAddress? GetFirstAddressByUserId(string userId);

   }
}
