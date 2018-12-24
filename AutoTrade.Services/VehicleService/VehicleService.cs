using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;

namespace AutoTrade.Services.VehicleService
{
	public class VehicleService : BaseService, IVehicleService
	{
		public VehicleService(AppDbContext dbContext) : base(dbContext)
		{ }

		public Guid AddVehicle(VehicleJsonModel model)
		{
			var vehicle = (Vehicle)this.Map(model, new Vehicle());

			DbContext.Vehicles.Add(vehicle);
			DbContext.SaveChanges();

			return vehicle.Id;
		}

		public VehicleJsonModel GetEmptyVehicle()
		{
			var makes = new VehicleMakeJsonModel
			{

			};


			var vehicle = new VehicleJsonModel
			{

			};

			return vehicle;
		}






		public void AddMake(VehicleMakeJsonModel model)
		{
			var make = (VehicleMake)this.Map(model, new VehicleMake());
			DbContext.VehicleMakes.Add(make);
			DbContext.SaveChanges();
		}

		public void AddModel(VehicleModelJsonModel model)
		{
			var vehicleModel = (VehicleModel)this.Map(model, new VehicleModel());
			DbContext.VehicleModels.Add(vehicleModel);
			DbContext.SaveChanges();
		}


		public IEnumerable<VehicleMakeJsonModel> GetMakes()
		{
			
		}

		public IEnumerable<VehicleMakeJsonModel> GetModels(int makeId)
		{

		}

		public void AddTown(TownJsonModel model)
		{
			var town = (Town)this.Map(model, new Town());
			DbContext.Towns.Add(town);
			DbContext.SaveChanges();
		}

		public void AddColor(ColorJsonModel model)
		{
			var color = (Color)this.Map(model, new Color());
			DbContext.Colors.Add(color);
			DbContext.SaveChanges();
		}

		public IEnumerable<TownJsonModel> GetTowns()
		{

		}

		public IEnumerable<ColorJsonModel> GetColors()
		{

		}

	}
}
