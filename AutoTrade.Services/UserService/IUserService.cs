﻿using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.UserService
{
	public interface IUserService
	{
		UserJsonModel GetByEmail(string email);
		UserJsonModel GetById(string id);
		bool IsExists(string email);
		string GetUserName(string email);
	}
}