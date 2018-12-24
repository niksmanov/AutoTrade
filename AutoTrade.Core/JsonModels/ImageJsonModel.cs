using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class ImageJsonModel
	{
		public int Id { get; set; }
		public int Name { get; set; }
		public Guid VehicleId { get; set; }
	}
}
