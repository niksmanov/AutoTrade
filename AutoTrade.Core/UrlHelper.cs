using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Core
{
	public static class UrlHelper
	{
		public static string GenerateVehicleUrl(Guid id)
		{
			return $"/vehicle/{id}";
		}

		public static string GenerateVehicleImageUrl(Guid vehicleId, string imageName)
		{
			if (!string.IsNullOrEmpty(imageName))
				return $"/images/{vehicleId}/{imageName}.png";

			return "/images/default-vehicle-logo.png";
		}
	}
}
