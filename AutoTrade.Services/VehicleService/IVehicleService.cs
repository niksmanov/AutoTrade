using System;
using System.Collections.Generic;
using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services
{
	public interface IVehicleService
	{
		Guid AddVehicle(VehicleJsonModel model);
		Guid EditVehicle(VehicleJsonModel model);
		bool RemoveVehicle(Guid id);
		bool AddMake(VehicleMakeJsonModel model);
		bool RemoveMake(int id);
		bool AddModel(VehicleModelJsonModel model);
		bool RemoveModel(int id);

		IEnumerable<VehicleJsonModel> SearchVehicles();
		IEnumerable<VehicleJsonModel> GetVehicles(string userId);
		VehicleJsonModel GetVehicle(Guid id);
		IEnumerable<VehicleMakeJsonModel> GetMakes();
		IEnumerable<VehicleModelJsonModel> GetModels(int makeId, int vehicleTypeId);
	}
}