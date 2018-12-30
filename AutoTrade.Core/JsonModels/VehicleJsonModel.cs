using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class VehicleJsonModel
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }

		public int MakeId { get; set; }
		public string Make { get; set; }
		public int ModelId { get; set; }
		public string Model { get; set; }
		public int ColorId { get; set; }
		public string Color { get; set; }


		public int TypeId { get; set; }
		public string Type { get; set; }
		public int FuelTypeId { get; set; }
		public string FuelType { get; set; }
		public int GearboxTypeId { get; set; }
		public string GearboxType { get; set; }


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

		public string Url { get; set; }
	}
}
