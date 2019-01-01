using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class Image
	{
		public Guid Id { get; set; }

		public virtual Vehicle Vehicle { get; set; }
		public Guid VehicleId { get; set; }
	}
}
