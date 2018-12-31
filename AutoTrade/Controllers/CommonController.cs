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
	public class CommonController : Controller
	{
		private readonly ICommonService _commonService;

		public CommonController(ICommonService commonService)
		{
			_commonService = commonService;
		}

		[HttpGet("[action]")]
		public IActionResult GetTowns()
		{
			var result = _commonService.GetTowns();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetColors()
		{
			var result = _commonService.GetColors();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetVehicleTypes()
		{
			var result = _commonService.GetVehicleTypes();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetFuelTypes()
		{
			var result = _commonService.GetFuelTypes();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetGearboxTypes()
		{
			var result = _commonService.GetGearboxTypes();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetAllCommons()
		{
			var result = _commonService.GetAllCommons();
			return Json(new ResponseJsonModel(true, result));
		}

		[HttpGet("[action]")]
		public IActionResult GetImages(Guid vehicleId)
		{
			var result = _commonService.GetImages(vehicleId);
			return Json(new ResponseJsonModel(true, result));
		}
	}
}