using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class Image
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public virtual Vehicle Vehicle { get; set; }
		public Guid VehicleId { get; set; }
		public byte[] Data { get; set; }
	}
}
