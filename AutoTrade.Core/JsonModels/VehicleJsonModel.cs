using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace AutoTrade.Core.JsonModels
{
	public class VehicleJsonModel
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public UserJsonModel User { get; set; }

		[Required]
		public int MakeId { get; set; }
		public string Make { get; set; }
		[Required]
		public int ModelId { get; set; }
		public string Model { get; set; }
		[Required]
		public int ColorId { get; set; }
		public string Color { get; set; }

		[Required]
		public int TypeId { get; set; }
		public string Type { get; set; }
		[Required]
		public int FuelTypeId { get; set; }
		public string FuelType { get; set; }
		[Required]
		public int GearboxTypeId { get; set; }
		public string GearboxType { get; set; }

		[Required]
		public int HorsePower { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int CubicCapacity { get; set; }

		[Required]
		public bool Airbags { get; set; }
		[Required]
		public bool ABS { get; set; }
		[Required]
		public bool ESP { get; set; }
		[Required]
		public bool CentralLocking { get; set; }
		[Required]
		public bool AirConditioning { get; set; }
		[Required]
		public bool AutoPilot { get; set; }

		[Required]
		public DateTime ProductionDate { get; set; } = new DateTime(1900, 1, 1);
		public string DisplayDate { get { return this.ProductionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); } }


		public string Url { get; set; }
		public string CoverImageUrl { get; set; }

		public IEnumerable<IFormFile> UploadImages { get; set; } = new List<IFormFile>();

		public IEnumerable<ImageJsonModel> Images { get; set; } = new List<ImageJsonModel>();
		public DateTime DateCreated { get; set; }
	}
}
