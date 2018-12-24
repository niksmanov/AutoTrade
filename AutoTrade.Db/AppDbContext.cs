using AutoTrade.Db.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Db
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
			this.Database.Migrate();
		}

		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<VehicleMake> VehicleMakes { get; set; }
		public DbSet<VehicleModel> VehicleModels { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Town> Towns { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>()
				   .HasOne(e => e.Town)
				   .WithOne();

			builder.Entity<User>()
				   .HasMany(e => e.Vehicles)
				   .WithOne(e => e.User)
				   .HasForeignKey(e => e.UserId);

			builder.Entity<VehicleMake>()
				   .HasMany(e => e.Models)
				   .WithOne(e => e.Make)
				   .OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(builder);
		}
	}
}
