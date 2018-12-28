using AutoTrade.Db.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class Vehicle
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public virtual User User { get; set; }

		public int MakeId { get; set; }
		public virtual VehicleMake Make { get; set; }
		public int ModelId { get; set; }
		public int ColorId { get; set; }
		public virtual Color Color { get; set; }


		public VehicleTypes Type { get; set; }
		public FuelType FuelType { get; set; }
		public GearboxType Gearbox { get; set; }

		public DateTime ProductionDate { get; set; }
		public int HorsePower { get; set; }
		public decimal Price { get; set; }
		public int CubicCapacity { get; set; }
		public int Airbag { get; set; }

		public bool ABS { get; set; }
		public bool ESP { get; set; }
		public bool CentralLocking { get; set; }
		public bool AirConditioning { get; set; }
		public bool AutoPilot { get; set; }

		public DateTime DateCreated { get; set; }

		public virtual ICollection<Image> Images { get; set; }

		public Vehicle()
		{
			this.Images = new HashSet<Image>();
		}
	}
}
