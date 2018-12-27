using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTrade.Infrastructure
{
	public static class ApplicationBuilder
	{
		public static async void SeedDatabase(this IApplicationBuilder app)

		{
			Role[] roles =
			{
				 new Role { Name = UserRoles.User.ToString() },
				 new Role { Name = UserRoles.Admin.ToString() }
			};

			var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

			using (var scope = serviceFactory.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role.Name))
					{
						await roleManager.CreateAsync(role);
					}
				}

				var user = new User { Email = "admin@autotrade.com", UserName = "Admin", EmailConfirmed = true };
				if (await userManager.FindByEmailAsync(user.Email) == null)
				{
					await userManager.CreateAsync(user, "password");
					await userManager.AddToRoleAsync(user, roles[1].Name);
				}
			}
		}
	}
}
