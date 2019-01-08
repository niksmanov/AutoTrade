using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class SearchVehiclesJsonModel
	{
		public int Page { get; set; }
		public int Size { get; set; }

		public int? TownId { get; set; }
		public int? MakeId { get; set; }
		public int? ModelId { get; set; }

		public int? ColorId { get; set; }
		public int? TypeId { get; set; }
		public int? FuelTypeId { get; set; }
		public int? GearboxTypeId { get; set; }

		public bool? Airbags { get; set; }
		public bool? ABS { get; set; }
		public bool? ESP { get; set; }
		public bool? CentralLocking { get; set; }
		public bool? AirConditioning { get; set; }
		public bool? AutoPilot { get; set; }


		public int? FromCubicCapacity { get; set; }
		public int? ToCubicCapacity { get; set; }

		public int? FromHorsePower { get; set; }
		public int? ToHorsePower { get; set; }

		public decimal? FromPrice { get; set; }
		public decimal? ToPrice { get; set; }

		public DateTime? FromProductionDate { get; set; }
		public DateTime? ToProductionDate { get; set; }
	}
}
