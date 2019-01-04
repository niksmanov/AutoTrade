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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleMake(int id)
		{
			bool isDeleted = _vehicleService.RemoveMake(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleModel(int id)
		{
			bool isDeleted = _vehicleService.RemoveModel(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveTown(int id)
		{
			bool isDeleted = _commonService.RemoveTown(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveColor(int id)
		{
			bool isDeleted = _commonService.RemoveColor(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveVehicleType(int id)
		{
			bool isDeleted = _commonService.RemoveVehicleType(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveFuelType(int id)
		{
			bool isDeleted = _commonService.RemoveFuelType(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
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
			string error = isAdded ? Messages.INFO_ENTITY_ADDED : Messages.ERROR_ENTITY_EXISTS;
			return Json(new ResponseJsonModel(isAdded, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveGearboxType(int id)
		{
			bool isDeleted = _commonService.RemoveGearboxType(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult ChangeRole(UserJsonModel model)
		{
			bool isChanged = _userService.ChangeRole(model);
			string error = isChanged ? Messages.INFO_ENTITY_EDITED : Messages.ERROR_EDIT_PROBLEM;
			return Json(new ResponseJsonModel(isChanged, error: error));
		}

		[HttpPost("[action]")]
		public IActionResult RemoveUser(string id)
		{
			bool isDeleted = _userService.RemoveUser(id);
			string error = isDeleted ? Messages.INFO_ENTITY_DELETED : Messages.ERROR_DELETE_PROBLEM;
			return Json(new ResponseJsonModel(isDeleted, error: error));
		}

		[HttpGet("[action]")]
		public IActionResult GetUsers(string search)
		{
			var users = _userService.GetUsers(search);
			return Json(new ResponseJsonModel(true, users));
		}
	}
}