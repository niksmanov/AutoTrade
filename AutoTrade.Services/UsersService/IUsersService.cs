using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.UsersService
{
	public interface IUsersService
	{
		void CreateAccount(UserJsonModel model);
		bool ValidatePassword(string email, string password);
	}
}