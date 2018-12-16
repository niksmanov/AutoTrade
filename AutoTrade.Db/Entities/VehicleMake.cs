using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class VehicleMake
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<VehicleModel> Models { get; set; }

		public VehicleMake()
		{
			this.Models = new HashSet<VehicleModel>();
		}
	}
}
