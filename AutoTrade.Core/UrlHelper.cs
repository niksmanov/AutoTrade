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
				return $"/images/{vehicleId}/{imageName}";

			return "/images/default-vehicle-logo.png";
		}

		public static string GenerateVehicleImageUrl(Guid vehicleId, byte[] data)
		{
			if (data != null && data.Length > 0)
			{
				var base64 = Convert.ToBase64String(data);
				return string.Format("data:image/png;base64,{0}", base64);
			}

			return "/images/default-vehicle-logo.png";
		}
	}
}
