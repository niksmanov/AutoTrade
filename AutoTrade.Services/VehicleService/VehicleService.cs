using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using AutoTrade.Db.QueryBuilders;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Services
{
	public class VehicleService : BaseService, IVehicleService
	{
		public VehicleService(AppDbContext dbContext) : base(dbContext)
		{ }


		public Guid AddVehicle(VehicleJsonModel model)
		{
			var vehicle = Map(model, new Vehicle());
			vehicle.DateCreated = DateTime.UtcNow;

			DbContext.Vehicles.Add(vehicle);
			DbContext.SaveChanges();

			return vehicle.Id;
		}

		public Guid EditVehicle(VehicleJsonModel model)
		{
			var dbVehicle = DbContext.Vehicles
									 .Include(i => i.Images)
									 .SingleOrDefault(c => c.Id == model.Id);

			if (dbVehicle != null)
			{
				dbVehicle = Map(model, dbVehicle);
				DbContext.Images.RemoveRange(dbVehicle.Images);
				DbContext.SaveChanges();
			}

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

				string filePath = Path.Combine(Directory.GetCurrentDirectory(), "App", "public", "images", $"{vehicle.Id}");
				if (Directory.Exists(filePath))
					Directory.Delete(filePath, true);

				return true;
			}
			return false;
		}

		public IEnumerable<VehicleJsonModel> GetVehicles(int page, int size,
			string userId, SearchVehiclesJsonModel search = null)
		{
			var query = DbContext.Vehicles
								 .Include(v => v.User)
								 .Include(v => v.Images)
								 .Include(v => v.Make)
												.ThenInclude(m => m.Models)
								 .Include(v => v.Color)
								 .Include(v => v.Type)
								 .Include(v => v.FuelType)
								 .Include(v => v.GearboxType)
								 .AsNoTracking();

			if (!string.IsNullOrEmpty(userId))
				query = query.Where(u => u.UserId == userId);

			if (search != null)
				query = BuildSearchQuery(query, search);

			return query.Skip(page * size)
						.Take(size)
						.OrderByDescending(u => u.DateCreated)
						.Select(v => Map(v, MapRelatedEntities(v)));
		}

		public VehicleJsonModel GetVehicle(Guid id)
		{
			var dbModel = DbContext.Vehicles
								   .Include(v => v.User)
												  .ThenInclude(u => u.Town)
								   .Include(v => v.Images)
								   .Include(v => v.Make)
												  .ThenInclude(m => m.Models)
								   .Include(v => v.Color)
								   .Include(v => v.Type)
								   .Include(v => v.FuelType)
								   .Include(v => v.GearboxType)
								   .SingleOrDefault(v => v.Id == id);

			return Map(dbModel, MapRelatedEntities(dbModel));
		}

		public bool AddMake(VehicleMakeJsonModel model)
		{
			var make = DbContext.VehicleMakes
								.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (make == null)
			{
				make = Map(model, new VehicleMake());
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
							.Select(m => Map(m, new VehicleMakeJsonModel()));
		}

		public bool AddModel(VehicleModelJsonModel model)
		{
			var vehicleModel = DbContext.VehicleModels
										.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (vehicleModel == null)
			{
				vehicleModel = Map(model, new VehicleModel { VehicleTypeId = model.VehicleTypeId });
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

		public IEnumerable<VehicleModelJsonModel> GetModels(int makeId, int vehicleTypeId)
		{
			var make = DbContext.VehicleMakes
								.Include(m => m.Models)
								.AsNoTracking()
								.SingleOrDefault(m => m.Id == makeId);

			if (make.Models.Any() && vehicleTypeId > 0)
				make.Models = make.Models
								  .Where(m => m.VehicleTypeId == vehicleTypeId)
								  .ToList();

			return make?.Models
						.OrderBy(m => m.Name)
						.Select(m => Map(m, new VehicleModelJsonModel { VehicleTypeId = m.VehicleTypeId }));
		}

		private VehicleJsonModel MapRelatedEntities(Vehicle vehicle)
		{
			if (vehicle != null)
			{
				return new VehicleJsonModel
				{
					DateCreated = vehicle.DateCreated,
					User = Map(vehicle.User, new UserJsonModel { TownName = vehicle.User?.Town?.Name }),
					Make = vehicle.Make.Name,
					Model = vehicle.Make.Models.SingleOrDefault(x => x.Id == vehicle.ModelId).Name,
					Color = vehicle.Color.Name,
					Type = vehicle.Type.Name,
					FuelType = vehicle.FuelType.Name,
					GearboxType = vehicle.GearboxType.Name,
					Url = UrlHelper.GenerateVehicleUrl(vehicle.Id),
					CoverImageUrl = UrlHelper.GenerateVehicleImageUrl(vehicle.Id, vehicle.Images.FirstOrDefault()?.Data),
					Images = vehicle.Images.Select(i =>
					Map(i, new ImageJsonModel { Url = UrlHelper.GenerateVehicleImageUrl(vehicle.Id, i.Data) })),
				};
			}
			return null;
		}

		private IQueryable<Vehicle> BuildSearchQuery(IQueryable<Vehicle> query, SearchVehiclesJsonModel search)
		{
			var queryBuilder = new SearchVehiclesQueryBuilder(search);

			query = queryBuilder.FilterTown(query);
			query = queryBuilder.FilterMake(query);
			query = queryBuilder.FilterModel(query);
			query = queryBuilder.FilterColor(query);
			query = queryBuilder.FilterType(query);
			query = queryBuilder.FilterFuelType(query);
			query = queryBuilder.FilterGearboxType(query);
			query = queryBuilder.FilterAirbags(query);
			query = queryBuilder.FilterABS(query);
			query = queryBuilder.FilterESP(query);
			query = queryBuilder.FilterCentralLocking(query);
			query = queryBuilder.FilterAirConditioning(query);
			query = queryBuilder.FilterAutoPilot(query);
			query = queryBuilder.FilterFromCubicCapacity(query);
			query = queryBuilder.FilterToCubicCapacity(query);
			query = queryBuilder.FilterFromHorsePower(query);
			query = queryBuilder.FilterToHorsePower(query);
			query = queryBuilder.FilterFromPrice(query);
			query = queryBuilder.FilterToPrice(query);
			query = queryBuilder.FilterFromProductionDate(query);
			query = queryBuilder.FilterToProductionDate(query);

			return query;
		}
	}
}
