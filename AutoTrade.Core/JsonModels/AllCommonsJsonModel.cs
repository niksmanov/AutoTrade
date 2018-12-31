using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class AllCommonsJsonModel
	{
		public IEnumerable<CommonJsonModel> Colors { get; set; } = new List<CommonJsonModel>();
		public IEnumerable<CommonJsonModel> Towns { get; set; } = new List<CommonJsonModel>();
		public IEnumerable<CommonJsonModel> VehicleTypes { get; set; } = new List<CommonJsonModel>();
		public IEnumerable<CommonJsonModel> FuelTypes { get; set; } = new List<CommonJsonModel>();
		public IEnumerable<CommonJsonModel> GearboxTypes { get; set; } = new List<CommonJsonModel>();
	}
}
