using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using AutoTrade.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;

		public UserController(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			IUserService userService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_userService = userService;
		}


		[HttpGet("[action]")]
		public IActionResult Current()
		{
			string id = _userManager.GetUserId(HttpContext.User);
			var user = _userService.GetById(id);

			if (user != null)
			{
				user.IsAdmin = User.IsInRole(UserRoles.PowerUser);
				return Json(new ResponseJsonModel(true, user));
			}
			return Json(new ResponseJsonModel());
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.UserName, Email = model.Email, LockoutEnabled = false };
				var isExist = _userService.IsExists(model.Email);
				if (isExist)
					return Json(new ResponseJsonModel(false, errors: Errors.EMAIL_EXISTS));

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					await _signInManager.SignInAsync(user, isPersistent: true);

				var res = new ResponseJsonModel(result.Succeeded);
				res.Errors = result.Errors.Select(e => e.Description).ToList();
				return Json(res);
			}
			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(false, errors: errors));
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var isExist = _userService.IsExists(model.Email);
				if (isExist)
				{
					string userName = _userService.GetUserName(model.Email);
					var result = await _signInManager.PasswordSignInAsync(
					userName, model.Password, model.RememberMe, lockoutOnFailure: false);

					if (result.Succeeded)
						return Json(new ResponseJsonModel(true));
					else
						return Json(new ResponseJsonModel(false, errors: Errors.INVALID_PASSWORD));
				}
				return Json(new ResponseJsonModel(false, errors: Errors.INVALID_EMAIL));
			}
			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(false, errors: errors));
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Json(new ResponseJsonModel(true));
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> ResetPassword(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, code, model.Password);

				var res = new ResponseJsonModel(result.Succeeded);
				res.Errors = result.Errors.Select(e => e.Description).ToList();
				return Json(res);
			}
			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(false, errors: errors));
		}
	}
}