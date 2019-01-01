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
	public class CommonService : BaseService, ICommonService
	{
		public CommonService(AppDbContext dbContext) : base(dbContext)
		{ }

		public bool AddTown(CommonJsonModel model)
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

		public IEnumerable<CommonJsonModel> GetTowns()
		{
			return DbContext.Towns?
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (CommonJsonModel)this.Map(m, new CommonJsonModel()));
		}

		public bool AddColor(CommonJsonModel model)
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

		public IEnumerable<CommonJsonModel> GetColors()
		{
			return DbContext.Colors?
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (CommonJsonModel)this.Map(m, new CommonJsonModel()));
		}

		public bool AddVehicleType(CommonJsonModel model)
		{
			var vehicleType = DbContext.VehicleTypes
									   .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (vehicleType == null)
			{
				vehicleType = (VehicleType)this.Map(model, new VehicleType());
				DbContext.VehicleTypes.Add(vehicleType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveVehicleType(int id)
		{
			var vehicleType = DbContext.VehicleTypes
									   .SingleOrDefault(c => c.Id == id);

			if (vehicleType != null)
			{
				DbContext.VehicleTypes.Remove(vehicleType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<CommonJsonModel> GetVehicleTypes()
		{
			return DbContext.VehicleTypes?
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (CommonJsonModel)this.Map(m, new CommonJsonModel()));
		}

		public bool AddFuelType(CommonJsonModel model)
		{
			var fuelType = DbContext.FuelTypes
									.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (fuelType == null)
			{
				fuelType = (FuelType)this.Map(model, new FuelType());
				DbContext.FuelTypes.Add(fuelType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveFuelType(int id)
		{
			var fuelType = DbContext.FuelTypes
									.SingleOrDefault(c => c.Id == id);

			if (fuelType != null)
			{
				DbContext.FuelTypes.Remove(fuelType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<CommonJsonModel> GetFuelTypes()
		{
			return DbContext.FuelTypes?
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (CommonJsonModel)this.Map(m, new CommonJsonModel()));
		}

		public bool AddGearboxType(CommonJsonModel model)
		{
			var gearboxType = DbContext.GearboxTypes
									   .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (gearboxType == null)
			{
				gearboxType = (GearboxType)this.Map(model, new GearboxType());
				DbContext.GearboxTypes.Add(gearboxType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public bool RemoveGearboxType(int id)
		{
			var gearboxType = DbContext.GearboxTypes
									   .SingleOrDefault(c => c.Id == id);

			if (gearboxType != null)
			{
				DbContext.GearboxTypes.Remove(gearboxType);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<CommonJsonModel> GetGearboxTypes()
		{
			return DbContext.GearboxTypes?
							.AsNoTracking()
							.OrderBy(m => m.Name)
							.Select(m => (CommonJsonModel)this.Map(m, new CommonJsonModel()));
		}

		public AllCommonsJsonModel GetAllCommons()
		{
			return new AllCommonsJsonModel
			{
				Colors = GetColors(),
				Towns = GetTowns(),
				VehicleTypes = GetVehicleTypes(),
				FuelTypes = GetFuelTypes(),
				GearboxTypes = GetGearboxTypes(),
			};
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

		public bool RemoveImage(Guid id)
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


		private IEnumerable<CommonJsonModel> EnumToJsonModel(Type enumType)
		{
			var result = new List<CommonJsonModel>();
			int[] values = (int[])Enum.GetValues(enumType);
			string[] names = Enum.GetNames(enumType);

			for (int i = 0; i < names.Length; i++)
			{
				result.Add(new CommonJsonModel
				{
					Id = values[i],
					Name = names[i]
				});
			}
			return result;
		}
	}
}
