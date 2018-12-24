using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using AutoTrade.Services.VehicleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class ProfileController : Controller
    {
		private readonly IVehicleService _vehicleService;

		public ProfileController(IVehicleService vehicleService)
		{
			_vehicleService = vehicleService;
		}

		[HttpGet("[action]")]
		public IActionResult Index()
        {
			//https://www.npmjs.com/package/react-infinite-scroller
			return Json(new ResponseJsonModel(true));
		}

		[HttpPost("[action]")]
		public IActionResult AddVehicle(VehicleJsonModel model)
		{
			var id = _vehicleService.AddVehicle(model);
			return Json(new ResponseJsonModel(true, id));
		}


		[HttpGet("[action]")]
		public IActionResult GetVehicleForm()
		{
			return Json(new ResponseJsonModel(true));
		}
	}
}