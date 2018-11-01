using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Services.UsersService;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : Controller
	{
		private readonly IUsersService _usersService;

		public AccountController(IUsersService usersService)
		{
			this._usersService = usersService;
		}

		[HttpGet("[action]")]
		public void Register()
		{
			var model = new UserJsonModel
			{
				Email = "test@abv.bg",
				Password = "12345"
			};
			this._usersService.CreateAccount(model);
		}

		[HttpGet("[action]")]
		public bool CheckPassword(UserJsonModel model)
		{
			return this._usersService.ValidatePassword(model.Email, model.Password);
		}

	}
}