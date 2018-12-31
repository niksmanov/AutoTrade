using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Route("[controller]")]
	public class VehicleController : Controller
	{
		private readonly IVehicleService _vehicleService;

		public VehicleController(IVehicleService vehicleService)
		{
			_vehicleService = vehicleService;
		}


		[HttpGet("[action]")]
		public IActionResult GetVehicleMakes()
		{
			var result = _vehicleService.GetMakes();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicleModels(int makeId, int? vehicleTypeId)
		{
			if (makeId > 0)
			{
				var result = _vehicleService.GetModels(makeId, vehicleTypeId);
				return Json(new ResponseJsonModel(true, result));
			}
			return Json(new ResponseJsonModel());
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicle(Guid? id)
		{
			var vehicle = new VehicleJsonModel();
			if (id.HasValue)
				vehicle = _vehicleService.GetVehicle(id.Value);

			return Json(new ResponseJsonModel(true, vehicle));
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicles(string userId)
		{
			var vehicles = _vehicleService.GetVehicles(userId);
			return Json(new ResponseJsonModel(true, vehicles));
		}
	}
}