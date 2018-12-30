using System;
using System.Collections.Generic;
using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.VehicleService
{
	public interface IVehicleService
	{
		Guid AddVehicle(VehicleJsonModel model);
		Guid EditVehicle(VehicleJsonModel model);
		bool RemoveVehicle(Guid id);
		bool AddColor(ColorJsonModel model);
		bool RemoveColor(int id);
		bool AddMake(VehicleMakeJsonModel model);
		bool RemoveMake(int id);
		bool AddModel(VehicleModelJsonModel model);
		bool RemoveModel(int id);
		bool AddTown(TownJsonModel model);
		bool RemoveTown(int id);
		bool AddImage(ImageJsonModel model);
		bool RemoveImage(int id);

		IEnumerable<VehicleJsonModel> SearchVehicles();
		IEnumerable<VehicleJsonModel> GetVehicles(string userId);
		VehicleJsonModel GetVehicle(Guid id);
		IEnumerable<ColorJsonModel> GetColors();
		IEnumerable<VehicleMakeJsonModel> GetMakes();
		IEnumerable<VehicleModelJsonModel> GetModels(int makeId, int? vehicleType);
		IEnumerable<TownJsonModel> GetTowns();
		IEnumerable<ImageJsonModel> GetImages(Guid vehicleId);
		VehicleEnumsJsonModel GetVehicleEnums();
	}
}