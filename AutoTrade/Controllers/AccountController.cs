using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Services.UsersService;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Route("[controller]")]
	public class AccountController : Controller
	{
		private readonly IUsersService _usersService;

		public AccountController(IUsersService usersService)
		{
			this._usersService = usersService;
		}

		[HttpPost("[action]")]
		public bool Register(UserJsonModel model)
		{
			return this._usersService.CreateAccount(model);
		}

		[HttpGet("[action]")]
		public bool ValidatePassword(UserJsonModel model)
		{
			return this._usersService.IsPasswordValid(model.Email, model.Password);
		}

	}
}