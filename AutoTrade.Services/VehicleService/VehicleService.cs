using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services.VehicleService
{
	public class VehicleService : BaseService, IVehicleService
	{
		public VehicleService(AppDbContext dbContext) : base(dbContext)
		{ }


		public IEnumerable<VehicleJsonModel> SearchVehicles()
		{
			throw new NotImplementedException();
		}

		public Guid AddVehicle(VehicleJsonModel model)
		{
			var vehicle = (Vehicle)this.Map(model, new Vehicle());
			vehicle.DateCreated = DateTime.UtcNow;

			DbContext.Vehicles.Add(vehicle);
			DbContext.SaveChanges();

			return vehicle.Id;
		}

		public bool RemoveVehicle(Guid id)
		{
			var vehicle = DbContext.Vehicles
								   .SingleOrDefault(c => c.Id == id);

			if (vehicle != null)
			{
				DbContext.Vehicles.Remove(vehicle);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<VehicleJsonModel> GetVehicles(string userId)
		{
			var query = DbContext.Vehicles
								 .Include(v => v.Make)
												.ThenInclude(v => v.Models)
								 .Include(v => v.Color)
								 .Include(v => v.Images)
								 .AsQueryable();

			if (!string.IsNullOrEmpty(userId))
			{
				query = query.Where(u => u.UserId == userId);
			}

			return query.OrderByDescending(u => u.DateCreated)
						.Select(v => (VehicleJsonModel)this.Map(v, new VehicleJsonModel()));
		}

		public VehicleJsonModel GetVehicle(Guid id)
		{
			var dbModel = DbContext.Vehicles
								   .Include(v => v.Make)
												  .ThenInclude(v => v.Models)
								   .Include(v => v.Color)
								   .Include(v => v.Images)
								   .SingleOrDefault(v => v.Id == id);

			return (VehicleJsonModel)this.Map(dbModel, new VehicleJsonModel());
		}

		public bool AddMake(VehicleMakeJsonModel model)
		{
			var make = DbContext.VehicleMakes
								.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (make == null)
			{
				make = (VehicleMake)this.Map(model, new VehicleMake());
				DbContext.VehicleMakes.Add(make);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveMake(int id)
		{
			var make = DbContext.VehicleMakes
								.SingleOrDefault(c => c.Id == id);

			if (make != null)
			{
				DbContext.VehicleMakes.Remove(make);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<VehicleMakeJsonModel> GetMakes()
		{
			return DbContext.VehicleMakes?
							.OrderBy(m => m.Name)
							.Select(m => (VehicleMakeJsonModel)this.Map(m, new VehicleMakeJsonModel()));
		}

		public bool AddModel(VehicleModelJsonModel model)
		{
			var vehicleModel = DbContext.VehicleModels
										.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (vehicleModel == null)
			{
				vehicleModel = (VehicleModel)this.Map(model, new VehicleModel());
				DbContext.VehicleModels.Add(vehicleModel);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveModel(int id)
		{
			var vehicleModel = DbContext.VehicleModels
										.SingleOrDefault(c => c.Id == id);

			if (vehicleModel != null)
			{
				DbContext.VehicleModels.Remove(vehicleModel);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<VehicleModelJsonModel> GetModels(int makeId)
		{
			var make = DbContext.VehicleMakes
								.Include(m => m.Models)
								.SingleOrDefault(m => m.Id == makeId);

			return make?.Models
						.OrderBy(m => m.Name)
						.Select(m => (VehicleModelJsonModel)this.Map(m, new VehicleModelJsonModel()));
		}

		public bool AddTown(TownJsonModel model)
		{
			var town = DbContext.Towns
								.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (town == null)
			{
				town = (Town)this.Map(model, new Town());
				DbContext.Towns.Add(town);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveTown(int id)
		{
			var town = DbContext.Towns
								.SingleOrDefault(c => c.Id == id);

			if (town != null)
			{
				DbContext.Towns.Remove(town);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<TownJsonModel> GetTowns()
		{
			return DbContext.Towns?
							.OrderBy(m => m.Name)
							.Select(m => (TownJsonModel)this.Map(m, new TownJsonModel()));
		}

		public bool AddColor(ColorJsonModel model)
		{
			var color = DbContext.Colors
								 .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (color == null)
			{
				color = (Color)this.Map(model, new Color());
				DbContext.Colors.Add(color);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveColor(int id)
		{
			var color = DbContext.Colors
								 .SingleOrDefault(c => c.Id == id);

			if (color != null)
			{
				DbContext.Colors.Remove(color);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<ColorJsonModel> GetColors()
		{
			return DbContext.Colors?
							.OrderBy(m => m.Name)
							.Select(m => (ColorJsonModel)this.Map(m, new ColorJsonModel()));
		}
	}
}
