using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace AutoTrade.Core.JsonModels
{
	public class ImageJsonModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Guid VehicleId { get; set; }

		public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
	}
}
