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

		[HttpPost("[action]")]
		public IActionResult AddVehicleMake(VehicleMakeJsonModel model)
		{
			_vehicleService.AddMake(model);
			return Json(new ResponseJsonModel(true));
		}

		[HttpPost("[action]")]
		public IActionResult AddVehicleModel(VehicleModelJsonModel model)
		{
			_vehicleService.AddModel(model);
			return Json(new ResponseJsonModel(true));
		}

		[HttpPost("[action]")]
		public IActionResult AddTown(TownJsonModel model)
		{
			_vehicleService.AddTown(model);
			return Json(new ResponseJsonModel(true));
		}

		[HttpPost("[action]")]
		public IActionResult AddColor(ColorJsonModel model)
		{
			_vehicleService.AddColor(model);
			return Json(new ResponseJsonModel(true));
		}
	}
}