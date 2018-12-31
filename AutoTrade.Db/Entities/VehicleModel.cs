using AutoTrade.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class VehicleModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int VehicleTypeId { get; set; }
		public virtual VehicleType VehicleType { get; set; }

		public int MakeId { get; set; }
		public virtual VehicleMake Make { get; set; }
	}
}
