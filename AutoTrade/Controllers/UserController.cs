using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using AutoTrade.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
			IEmailSender emailSender,
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
			var user = _userService.GetUserById(id);
			var response = new ResponseJsonModel();

			if (user != null)
			{
				user.IsAdmin = User.IsInRole(UserRoles.PowerUser);
				response.Succeeded = true;
				response.Data = user;
			}
			return Json(response);
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.UserName, Email = model.Email, LockoutEnabled = false };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					await _signInManager.SignInAsync(user, isPersistent: true);

				return Json(new { succeeded = result.Succeeded, errors = result.Errors });
			}
			return BadRequest();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var response = _userService.IsUserExists(model.Email);
				if (response.Succeeded)
				{
					string userName = _userService.GetUserUserName(model.Email);
					var result = await _signInManager.PasswordSignInAsync(
					userName, model.Password, model.RememberMe, lockoutOnFailure: false);
					return Json(response);
				}
				return Json(response);
			}
			//var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
			return BadRequest();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Json(new ResponseJsonModel { Succeeded = true });
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> ResetPassword(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
				return Json(new { succeeded = result.Succeeded, errors = result.Errors });
			}
			return BadRequest();
		}

		[Authorize]
		[HttpGet("[action]")]
		public IActionResult Profile()
		{

			return Json(new ResponseJsonModel { Succeeded = true });
		}
	}
}