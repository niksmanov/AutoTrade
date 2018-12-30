using AutoTrade.Core.JsonModels;
using System.Collections.Generic;

namespace AutoTrade.Services.UserService
{
	public interface IUserService
	{
		UserJsonModel GetById(string id);
		bool IsExists(string email);
		string GetUserName(string email);
		bool RemoveUser(string id);
		IEnumerable<UserJsonModel> GetUsers(string search);
		bool ChangeRole(UserJsonModel model);
		bool EditUser(UserJsonModel model);
	}
}