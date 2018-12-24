using System;
using System.Collections.Generic;
using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services.VehicleService
{
	public interface IVehicleService
	{
		void AddColor(ColorJsonModel model);
		void AddMake(VehicleMakeJsonModel model);
		void AddModel(VehicleModelJsonModel model);
		void AddTown(TownJsonModel model);
		Guid AddVehicle(VehicleJsonModel model);
		IEnumerable<ColorJsonModel> GetColors();
		VehicleJsonModel GetEmptyVehicle();
		IEnumerable<VehicleMakeJsonModel> GetMakes();
		IEnumerable<VehicleMakeJsonModel> GetModels(int makeId);
		IEnumerable<TownJsonModel> GetTowns();
	}
}