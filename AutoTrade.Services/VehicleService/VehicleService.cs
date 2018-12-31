using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services
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


		public Guid EditVehicle(VehicleJsonModel model)
		{

			var dbVehicle = DbContext.Vehicles
									 .SingleOrDefault(c => c.Id == model.Id);

			dbVehicle = (Vehicle)this.Map(model, dbVehicle);
			DbContext.SaveChanges();

			return dbVehicle.Id;
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
								 .Include(v => v.Images)
								 .AsNoTracking();

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
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (VehicleMakeJsonModel)this.Map(m, new VehicleMakeJsonModel()));
		}

		public bool AddModel(VehicleModelJsonModel model)
		{
			var vehicleModel = DbContext.VehicleModels
										.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (vehicleModel == null)
			{
				vehicleModel = (VehicleModel)this.Map(model,
								new VehicleModel { VehicleTypeId = model.VehicleTypeId });
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

		public IEnumerable<VehicleModelJsonModel> GetModels(int makeId, int? vehicleTypeId)
		{
			var make = DbContext.VehicleMakes
								.Include(m => m.Models)
								.AsNoTracking()
								.SingleOrDefault(m => m.Id == makeId);

			if (make.Models.Any() && vehicleTypeId.HasValue)
			{
				make.Models = make.Models
								  .Where(m => m.VehicleTypeId == vehicleTypeId.Value)
								  .ToList();
			}

			return make?.Models
						.OrderBy(m => m.Name)
						.Select(m => (VehicleModelJsonModel)this.Map(m,
								new VehicleModelJsonModel { VehicleTypeId = m.VehicleTypeId }));
		}
	}
}
