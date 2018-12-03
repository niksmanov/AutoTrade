using System;
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
		private readonly IEmailSender _emailSender;

		public UserController(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			IUserService userService,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_userService = userService;
			_emailSender = emailSender;
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
					return Json(new ResponseJsonModel(false, error: Messages.ERROR_EMAIL_EXISTS));

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

					var callbackUrl = Url.RouteUrl("ConfirmEmail",
					 values: new UserJsonModel { Id = user.Id, Code = code },
					 protocol: Request.Scheme);

					await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
						"Please confirm your email by clicking here " + callbackUrl);
					await _signInManager.SignInAsync(user, isPersistent: true);
				}

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
				}
				return Json(new ResponseJsonModel(false, error: Messages.ERROR_INVALID_DATA));
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
		public async Task<IActionResult> ForgotPassword(UserJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
					return Json(new ResponseJsonModel(error: Messages.ERROR_INVALID_EMAIL));

				model.Code = await _userManager.GeneratePasswordResetTokenAsync(user);
				string newPassword = await this.ChangePassword(model);

				await _emailSender.SendEmailAsync(model.Email, "New Password",
						"Your new password is: " + newPassword);

				return Json(new ResponseJsonModel(error: Messages.INFO_EMAIL_SENT));
			}
			return Json(new ResponseJsonModel());
		}

		[HttpGet("[action]")]
		[Route("ConfirmEmail", Name = "ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmail(UserJsonModel model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);
			if (user != null)
				await _userManager.ConfirmEmailAsync(user, model.Code);
			return Redirect("/");
		}

		[Authorize]
		[HttpPost("[action]")]
		public async Task<IActionResult> ResetPassword(UserJsonModel model)
		{
			string loggedUserId = _userManager.GetUserId(HttpContext.User);
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (ModelState.IsValid && user != null && loggedUserId == user.Id)
			{
				model.Code = await _userManager.GeneratePasswordResetTokenAsync(user);
				await this.ChangePassword(model);
				return Json(new ResponseJsonModel(error: Messages.INFO_PASSWORD_CHANGED));
			}
			return Json(new ResponseJsonModel(error: Messages.ERROR_INVALID_EMAIL));
		}

		[NonAction]
		private async Task<string> ChangePassword(UserJsonModel model)
		{
			if (string.IsNullOrEmpty(model.Password))
				model.Password = Guid.NewGuid().ToString().Split('-')[0];

			var user = await _userManager.FindByEmailAsync(model.Email);
			await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
			return model.Password;
		}
	}
}