using System;
using System.Collections.Generic;
using System.Linq;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using AutoTrade.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class ProfileController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;
		private readonly IVehicleService _vehicleService;
		private readonly ICommonService _commonService;


		public ProfileController(
			UserManager<User> userManager,
			IUserService userService,
			IVehicleService vehicleService,
			ICommonService commonService)
		{
			_userManager = userManager;
			_userService = userService;
			_vehicleService = vehicleService;
			_commonService = commonService;
		}


		[HttpPost("[action]")]
		public IActionResult EditInfo(UserJsonModel model)
		{
			bool isEdited = false;
			string userId = _userManager.GetUserId(HttpContext.User);

			if (model.Id == userId)
				isEdited = _userService.EditUser(model);

			string error = isEdited ? Messages.INFO_ENTITY_EDITED : Messages.ERROR_EDIT_PROBLEM;
			return Json(new ResponseJsonModel(isEdited, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult AddVehicle(VehicleJsonModel model)
		{
			if (ModelState.IsValid)
			{
				bool isAdded = false;
				var id = _vehicleService.AddVehicle(model);
				isAdded = model.Id != id;

				if (model.UploadImages.Any() && isAdded)
				{
					model.Id = id;
					model.Images = _commonService.SaveImagesInDatabase(model);
					isAdded = _commonService.AddImages(model.Images);
				}
				string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ADD_PROBLEM;
				return Json(new ResponseJsonModel(isAdded, id, error: error));
			}

			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(errors: errors));
		}

		[HttpPost("[action]")]
		public IActionResult EditVehicle(VehicleJsonModel model)
		{
			string userId = _userManager.GetUserId(HttpContext.User);
			bool isAdmin = User.IsInRole(UserRoles.Admin.ToString());

			if (ModelState.IsValid && (model.UserId == userId || isAdmin))
			{
				bool isEdited = false;
				var id = _vehicleService.EditVehicle(model);
				isEdited = model.Id == id;
				if (model.UploadImages.Any() && isEdited)
				{
					model.Id = id;
					model.Images = _commonService.SaveImagesInDatabase(model);
					isEdited = _commonService.AddImages(model.Images);
				}
				string error = isEdited ? Messages.INFO_ENTITY_EDITED : Messages.ERROR_EDIT_PROBLEM;
				return Json(new ResponseJsonModel(isEdited, id, error: error));
			}

			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(errors: errors));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicle(Guid id)
		{
			bool isDeleted = false;
			string userId = _userManager.GetUserId(HttpContext.User);
			bool isAdmin = User.IsInRole(UserRoles.Admin.ToString());

			var vehicle = _vehicleService.GetVehicle(id);
			if (vehicle.UserId == userId || isAdmin)
				isDeleted = _vehicleService.RemoveVehicle(id);

			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
		}
	}
}