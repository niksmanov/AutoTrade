using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoTrade.Core;
using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using Microsoft.EntityFrameworkCore;
using ImageMagick;

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
				town = Map(model, new Town());
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
							.Select(m => Map(m, new CommonJsonModel()));
		}

		public bool AddColor(CommonJsonModel model)
		{
			var color = DbContext.Colors
								 .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (color == null)
			{
				color = Map(model, new Color());
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
							.Select(m => Map(m, new CommonJsonModel()));
		}

		public bool AddVehicleType(CommonJsonModel model)
		{
			var vehicleType = DbContext.VehicleTypes
									   .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (vehicleType == null)
			{
				vehicleType = Map(model, new VehicleType());
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
							.Select(m => Map(m, new CommonJsonModel()));
		}

		public bool AddFuelType(CommonJsonModel model)
		{
			var fuelType = DbContext.FuelTypes
									.SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (fuelType == null)
			{
				fuelType = Map(model, new FuelType());
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
							.Select(m => Map(m, new CommonJsonModel()));
		}

		public bool AddGearboxType(CommonJsonModel model)
		{
			var gearboxType = DbContext.GearboxTypes
									   .SingleOrDefault(c => c.Name.ToLower() == model.Name.ToLower());

			if (gearboxType == null)
			{
				gearboxType = Map(model, new GearboxType());
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
							.Select(m => Map(m, new CommonJsonModel()));
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

		public bool AddImages(IEnumerable<ImageJsonModel> images)
		{
			var dbImages = images.Select(i => Map(i, new Image()));
			DbContext.Images.AddRange(dbImages);
			DbContext.SaveChanges();
			return true;
		}

		public bool RemoveImages(IEnumerable<ImageJsonModel> images)
		{
			var dbImages = images.Select(i => Map(i, new Image()));
			DbContext.Images.RemoveRange(dbImages);
			DbContext.SaveChanges();
			return true;
		}

		public IEnumerable<ImageJsonModel> GetImages(Guid vehicleId)
		{
			return DbContext.Images
							.Where(i => i.VehicleId == vehicleId)
							.AsNoTracking()
							.Select(i => Map(i,
							new ImageJsonModel { Url = UrlHelper.GenerateVehicleImageUrl(vehicleId, i.Data) }));
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


		public IEnumerable<ImageJsonModel> SaveImagesOnFileSystem(VehicleJsonModel model)
		{
			const decimal MAX_SIZE_IN_BYTES = 1000000;
			var images = new List<ImageJsonModel>();
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "App", "public", "images", $"{model.Id}");

			if (Directory.Exists(filePath))
				foreach (var file in Directory.GetFiles(filePath))
					File.Delete(file);
			else
				Directory.CreateDirectory(filePath);

			foreach (var image in model.UploadImages.Take(10))
			{
				if (image.ContentType == "image/jpeg" ||
					image.ContentType == "image/png")
				{
					using (var compressedImg = new MagickImage(image.OpenReadStream()))
					{
						decimal quality = (MAX_SIZE_IN_BYTES / image.Length) * 100;
						compressedImg.Quality = (int)(quality > 90 ? 90 : quality);
						compressedImg.Format = MagickFormat.Jpg;

						string imageName = Guid.NewGuid().ToString() + ".jpg";
						compressedImg.Write($"{filePath}\\{imageName}");

						images.Add(new ImageJsonModel { Name = imageName, VehicleId = model.Id });
					}
				}
			}
			return images;
		}


		public IEnumerable<ImageJsonModel> SaveImagesInDatabase(VehicleJsonModel model)
		{
			const decimal MAX_SIZE_IN_BYTES = 1000000;
			var images = new List<ImageJsonModel>();

			foreach (var image in model.UploadImages.Take(10))
			{
				if (image.ContentType == "image/jpeg" ||
					image.ContentType == "image/png")
				{
					using (var compressedImg = new MagickImage(image.OpenReadStream()))
					{
						decimal quality = (MAX_SIZE_IN_BYTES / image.Length) * 100;
						compressedImg.Quality = (int)(quality > 90 ? 90 : quality);
						compressedImg.Format = MagickFormat.Jpg;

						string imageName = Guid.NewGuid().ToString() + ".jpg";
						images.Add(new ImageJsonModel { Data = compressedImg.ToByteArray(), VehicleId = model.Id });
					}
				}
			}
			return images;
		}
	}
}
