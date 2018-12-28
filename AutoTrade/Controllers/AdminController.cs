using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Enums;
using AutoTrade.Services.UserService;
using AutoTrade.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("[controller]")]
	public class AdminController : Controller
	{
		private readonly IUserService _userService;
		private readonly IVehicleService _vehicleService;


		public AdminController(
			IUserService userService,
			IVehicleService vehicleService)
		{
			_userService = userService;
			_vehicleService = vehicleService;
		}


		[HttpPost("[action]")]
		public IActionResult AddVehicleMake(VehicleMakeJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _vehicleService.AddMake(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleMake(int id)
		{
			bool isDeleted = _vehicleService.RemoveMake(id);
			return Json(new ResponseJsonModel(isDeleted));
		}


		[HttpPost("[action]")]
		public IActionResult AddVehicleModel(VehicleModelJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _vehicleService.AddModel(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleModel(int id)
		{
			bool isDeleted = _vehicleService.RemoveModel(id);
			return Json(new ResponseJsonModel(isDeleted));
		}


		[HttpPost("[action]")]
		public IActionResult AddTown(TownJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _vehicleService.AddTown(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveTown(int id)
		{
			bool isDeleted = _vehicleService.RemoveTown(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult AddColor(ColorJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _vehicleService.AddColor(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveColor(int id)
		{
			bool isDeleted = _vehicleService.RemoveColor(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult ChangeRole(UserJsonModel model)
		{
			bool isChanged = _userService.ChangeRole(model);
			return Json(new ResponseJsonModel(isChanged));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveUser(string id)
		{
			bool isDeleted = _userService.RemoveUser(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpGet("[action]")]
		public IActionResult GetUsers(string search)
		{
			var users = _userService.GetUsers(search);
			return Json(new ResponseJsonModel(true, users));
		}
	}
}