using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTrade
{
	public static class ApplicationBuilder
	{
		public static async void SeedDatabase(this IApplicationBuilder app)

		{
			IdentityRole[] roles =
			{
				 new IdentityRole(UserRoles.User),
				 new IdentityRole(UserRoles.PowerUser)
			};

			var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

			using (var scope = serviceFactory.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role.Name))
					{
						await roleManager.CreateAsync(role);
					}

				}

				var user = new User { Email = "admin@gmail.com", UserName = "Admin" };
				if (await userManager.FindByEmailAsync(user.Email) == null)
				{
					await userManager.CreateAsync(user, "password");
					await userManager.AddToRoleAsync(user, roles[1].Name);
				}
			}
		}
	}
}
