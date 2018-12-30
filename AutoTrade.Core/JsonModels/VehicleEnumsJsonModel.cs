using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class VehicleEnumsJsonModel
	{
		public IEnumerable<EnumJsonModel> VehicleType { get; set; } = new List<EnumJsonModel>();
		public IEnumerable<EnumJsonModel> FuelType { get; set; } = new List<EnumJsonModel>();
		public IEnumerable<EnumJsonModel> GearboxType { get; set; } = new List<EnumJsonModel>();
	}
}
