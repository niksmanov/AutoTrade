using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Entities;
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

		public ProfileController(
			UserManager<User> userManager,
			IUserService userService,
			IVehicleService vehicleService)
		{
			_userManager = userManager;
			_userService = userService;
			_vehicleService = vehicleService;
		}

		//TO DO: IMAGES ADD AND PREVIEW
		//Bootstrap carousel in modal, single vehicle page
		//TO DO: SEARCH QUERY WITH FORM!!!
		//TO DO: INFINITE SCROLL ON VEHICLES AND USERS https://www.npmjs.com/package/react-infinite-scroller


		[HttpPost("[action]")]
		public IActionResult EditInfo(UserJsonModel model)
		{
			bool isEdited = _userService.EditUser(model);
			return Json(new ResponseJsonModel(isEdited));
		}

		[HttpPost("[action]")]
		public IActionResult AddVehicle(VehicleJsonModel model)
		{
			if (ModelState.IsValid)
			{
				var id = _vehicleService.AddVehicle(model);
				return Json(new ResponseJsonModel(true, id));
			}

			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(errors: errors));
		}

		[HttpPost("[action]")]
		public IActionResult EditVehicle(VehicleJsonModel model)
		{
			string userId = _userManager.GetUserId(HttpContext.User);
			if (ModelState.IsValid && model.UserId == userId)
			{
				var id = _vehicleService.EditVehicle(model);
				return Json(new ResponseJsonModel(true, id));
			}

			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return Json(new ResponseJsonModel(errors: errors));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicle(Guid id)
		{
			bool isDeleted = false;
			string userId = _userManager.GetUserId(HttpContext.User);

			var vehicle = _vehicleService.GetVehicle(id);
			if (vehicle.UserId == userId)
				isDeleted = _vehicleService.RemoveVehicle(id);

			return Json(new ResponseJsonModel(isDeleted));
		}

		[NonAction]
		private List<string> SaveImagesOnFileSystem(ImageJsonModel model)
		{
			var imageNames = new List<string>();
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "App", "public", "images", $"{model.VehicleId}");

			if (!Directory.Exists(filePath))
				Directory.CreateDirectory(filePath);

			foreach (var image in model.Images.Take(10))
			{
				if (image.ContentType == "image/jpeg")
				{
					string imageName = Guid.NewGuid().ToString();
					filePath = filePath + $"/{imageName}.png";

					var fileStream = new FileStream(filePath, FileMode.Create);
					image.CopyTo(fileStream);
					fileStream.Close();

					imageNames.Add(imageName);
				}
			}
			return imageNames;
		}
	}
}