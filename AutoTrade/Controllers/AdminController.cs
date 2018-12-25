using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Enums;
using AutoTrade.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("[controller]")]
	public class AdminController : Controller
	{
		private readonly IVehicleService _vehicleService;

		public AdminController(IVehicleService vehicleService)
		{
			_vehicleService = vehicleService;
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicleMakes()
		{
			var result = _vehicleService.GetMakes();
			return Json(new ResponseJsonModel(true, result));
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

		[HttpGet("[action]")]
		public IActionResult RemoveVehicleMake(int id)
		{
			bool isDeleted = _vehicleService.RemoveMake(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicleModels(int makeId)
		{
			var result = _vehicleService.GetModels(makeId);
			return Json(new ResponseJsonModel(true, result));
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

		[HttpGet("[action]")]
		public IActionResult RemoveVehicleModel(int id)
		{
			bool isDeleted = _vehicleService.RemoveModel(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpGet("[action]")]
		public IActionResult GetTowns()
		{
			var result = _vehicleService.GetTowns();
			return Json(new ResponseJsonModel(true, result));
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

		[HttpGet("[action]")]
		public IActionResult RemoveTown(int id)
		{
			bool isDeleted = _vehicleService.RemoveTown(id);
			return Json(new ResponseJsonModel(isDeleted));
		}

		[HttpGet("[action]")]
		public IActionResult GetColors()
		{
			var result = _vehicleService.GetColors();
			return Json(new ResponseJsonModel(true, result));
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

		[HttpGet("[action]")]
		public IActionResult RemoveColor(int id)
		{
			bool isDeleted = _vehicleService.RemoveColor(id);
			return Json(new ResponseJsonModel(isDeleted));
		}
	}
}