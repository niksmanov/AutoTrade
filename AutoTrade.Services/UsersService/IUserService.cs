using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.UsersService
{
	public interface IUserService
	{
		UserJsonModel GetUserByEmail(string email);
		UserJsonModel GetUserById(string id);
		bool IsUserExists(string email);
	}
}