using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Enums;
using AutoTrade.Services;
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
		private readonly ICommonService _commonService;


		public AdminController(
			IUserService userService,
			IVehicleService vehicleService,
			ICommonService commonService)
		{
			_userService = userService;
			_vehicleService = vehicleService;
			_commonService = commonService;
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
		public IActionResult AddTown(CommonJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _commonService.AddTown(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveTown(int id)
		{
			bool isDeleted = _commonService.RemoveTown(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult AddColor(CommonJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _commonService.AddColor(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveColor(int id)
		{
			bool isDeleted = _commonService.RemoveColor(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult AddVehicleType(CommonJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _commonService.AddVehicleType(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleType(int id)
		{
			bool isDeleted = _commonService.RemoveVehicleType(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult AddFuelType(CommonJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _commonService.AddFuelType(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveFuelType(int id)
		{
			bool isDeleted = _commonService.RemoveFuelType(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpPost("[action]")]
		public IActionResult AddGearboxType(CommonJsonModel model)
		{
			bool isAdded = false;
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Name = model.Name.Trim();
				isAdded = _commonService.AddGearboxType(model);
			}

			return Json(new ResponseJsonModel(isAdded));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveGearboxType(int id)
		{
			bool isDeleted = _commonService.RemoveGearboxType(id);
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