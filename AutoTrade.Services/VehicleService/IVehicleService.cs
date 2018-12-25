using System;
using System.Collections.Generic;
using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.VehicleService
{
	public interface IVehicleService
	{
		bool AddColor(ColorJsonModel model);
		bool RemoveColor(int id);
		bool AddMake(VehicleMakeJsonModel model);
		bool RemoveMake(int id);
		bool AddModel(VehicleModelJsonModel model);
		bool RemoveModel(int id);
		bool AddTown(TownJsonModel model);
		bool RemoveTown(int id);
		Guid AddVehicle(VehicleJsonModel model);
		IEnumerable<ColorJsonModel> GetColors();
		IEnumerable<VehicleMakeJsonModel> GetMakes();
		IEnumerable<VehicleModelJsonModel> GetModels(int makeId);
		IEnumerable<TownJsonModel> GetTowns();
	}
}