using System;
using System.Collections.Generic;
using AutoTrade.Core.JsonModels;

namespace AutoTrade.Services
{
	public interface ICommonService
	{
		bool AddColor(CommonJsonModel model);
		bool AddTown(CommonJsonModel model);
		bool AddVehicleType(CommonJsonModel model);
		bool AddFuelType(CommonJsonModel model);
		bool AddGearboxType(CommonJsonModel model);
		bool AddImages(IEnumerable<ImageJsonModel> images);


		bool RemoveColor(int id);
		bool RemoveTown(int id);
		bool RemoveVehicleType(int id);
		bool RemoveFuelType(int id);
		bool RemoveGearboxType(int id);
		bool RemoveImages(IEnumerable<ImageJsonModel> images);


		IEnumerable<CommonJsonModel> GetColors();
		IEnumerable<CommonJsonModel> GetTowns();
		IEnumerable<CommonJsonModel> GetVehicleTypes();
		IEnumerable<CommonJsonModel> GetFuelTypes();
		IEnumerable<CommonJsonModel> GetGearboxTypes();

		IEnumerable<ImageJsonModel> GetImages(Guid vehicleId);
		AllCommonsJsonModel GetAllCommons();

		IEnumerable<ImageJsonModel> SaveImagesOnFileSystem(VehicleJsonModel model);
		IEnumerable<ImageJsonModel> SaveImagesInDatabase(VehicleJsonModel model);
	}
}