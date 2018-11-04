using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.UsersService
{
	public interface IUsersService
	{
		bool CreateAccount(UserJsonModel model);
		bool IsPasswordValid(string email, string password);
	}
}