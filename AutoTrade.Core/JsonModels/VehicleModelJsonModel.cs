using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class VehicleModelJsonModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int MakeId { get; set; }
		public int VehicleTypeId { get; set; }
	}
}
