using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class ProfileController : Controller
	{
		private readonly IUserService _userService;
		private readonly IVehicleService _vehicleService;

		public ProfileController(
			IUserService userService,
			IVehicleService vehicleService)
		{
			_userService = userService;
			_vehicleService = vehicleService;
		}

		//https://www.npmjs.com/package/react-infinite-scroller


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
			var id = _vehicleService.EditVehicle(model);
			return Json(new ResponseJsonModel(true, id));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicle(Guid id)
		{
			bool isDeleted = _vehicleService.RemoveVehicle(id);
			return Json(new ResponseJsonModel(isDeleted));
		}
	}
}