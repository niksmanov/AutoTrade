using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Services.UsersService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoTrade
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "App/build";
			});

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, IdentityRole>()
				.AddDefaultUI()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password = new PasswordOptions()
				{
					RequiredLength = 5,
					RequiredUniqueChars = 0,
					RequireLowercase = false,
					RequireDigit = false,
					RequireUppercase = false,
					RequireNonAlphanumeric = false,
				};
			});

			ConfigureDependencies(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseSpaStaticFiles();
			app.UseAuthentication();
			app.SeedDatabase();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "App";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}

		private void ConfigureDependencies(IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
		}
	}
}
