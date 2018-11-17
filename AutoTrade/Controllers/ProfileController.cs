using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrade.Core.JsonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTrade.Controllers
{
	[Authorize]
	public class ProfileController : Controller
    {
		[HttpGet("[action]")]
		public IActionResult Index()
        {
			return Json(new ResponseJsonModel(true));
		}
    }
}