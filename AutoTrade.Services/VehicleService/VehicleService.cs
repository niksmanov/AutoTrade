using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
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
								new VehicleModel { VehicleType = (VehicleType)model.VehicleType });
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

		public IEnumerable<VehicleModelJsonModel> GetModels(int makeId, int? vehicleType)
		{
			var make = DbContext.VehicleMakes
								.Include(m => m.Models)
								.AsNoTracking()
								.SingleOrDefault(m => m.Id == makeId);

			if (make.Models.Any() && vehicleType.HasValue)
			{
				make.Models = make.Models
								  .Where(m => (int)m.VehicleType == vehicleType.Value)
								  .ToList();
			}

			return make?.Models
						.OrderBy(m => m.Name)
						.Select(m => (VehicleModelJsonModel)this.Map(m,
								new VehicleModelJsonModel { VehicleType = (int)m.VehicleType }));
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
							.AsNoTracking()
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
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (ColorJsonModel)this.Map(m, new ColorJsonModel()));
		}

		public bool AddImage(ImageJsonModel model)
		{
			var image = DbContext.Images
								 .SingleOrDefault(i => i.Id == model.Id);

			if (image == null)
			{
				image = (Image)this.Map(model, new Image());
				DbContext.Images.Add(image);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveImage(int id)
		{
			var image = DbContext.Images
								 .SingleOrDefault(c => c.Id == id);

			if (image != null)
			{
				DbContext.Images.Remove(image);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<ImageJsonModel> GetImages(Guid vehicleId)
		{
			return DbContext.Images
							.Where(i => i.VehicleId == vehicleId)
							.AsNoTracking()
							.Select(i => (ImageJsonModel)this.Map(i, new ImageJsonModel()));
		}

		public VehicleEnumsJsonModel GetVehicleEnums()
		{
			var vehicleEnums = new VehicleEnumsJsonModel();

			vehicleEnums.VehicleType = EnumToJsonModel(typeof(VehicleType));
			vehicleEnums.FuelType = EnumToJsonModel(typeof(FuelType));
			vehicleEnums.GearboxType = EnumToJsonModel(typeof(GearboxType));

			return vehicleEnums;
		}

		private IEnumerable<EnumJsonModel> EnumToJsonModel(Type enumType)
		{
			var result = new List<EnumJsonModel>();
			int[] values = (int[])Enum.GetValues(enumType);
			string[] names = Enum.GetNames(enumType);

			for (int i = 0; i < names.Length; i++)
			{
				result.Add(new EnumJsonModel
				{
					Id = values[i],
					Name = names[i]
				});
			}
			return result;
		}
	}
}
