using AutoTrade.Db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Db
{
	public class AppDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
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
			base.OnModelCreating(builder);


			builder.Entity<UserRole>()
				   .HasKey(ur => new { ur.UserId, ur.RoleId });

			builder.Entity<UserRole>()
				   .HasOne(ur => ur.Role)
				   .WithMany(r => r.UserRoles)
				   .HasForeignKey(ur => ur.RoleId)
				   .IsRequired();

			builder.Entity<UserRole>()
				   .HasOne(ur => ur.User)
				   .WithMany(r => r.UserRoles)
				   .HasForeignKey(ur => ur.UserId)
				   .IsRequired();

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
		}
	}
}
