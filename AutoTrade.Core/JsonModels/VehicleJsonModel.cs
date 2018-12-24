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
		public ICollection<VehicleMakeJsonModel> Makes { get; set; }

		public int ModelId { get; set; }
		public ICollection<VehicleModelJsonModel> Models { get; set; }

		public string Type { get; set; }
		public string FuelType { get; set; }
		public string Color { get; set; }
		public string Gearbox { get; set; }

		public int HorsePower { get; set; }
		public int Year { get; set; }
		public decimal Price { get; set; }
		public int CubicCapacity { get; set; }
		public int Airbag { get; set; }

		public bool ABS { get; set; }
		public bool ESP { get; set; }
		public bool CentralLocking { get; set; }
		public bool AirConditioning { get; set; }
		public bool AutoPilot { get; set; }


		public ICollection<ImageJsonModel> Images { get; set; }

		public string Url { get; set; }
	}
}
