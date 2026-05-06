using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IUserService : IGenericServiceRepo<User>
	{
		User GetCurrentUser();
		User GetByUsername(string username);
		User GetByUserId(string userId);
		Task<bool> IsUserAdminAsync(User user);
	}
}
