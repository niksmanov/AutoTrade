using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTrade.Db;
using AutoTrade.Db.Entities;

namespace AutoTrade.Services.VehicleService
{
	public class VehicleService : BaseService, IVehicleService
	{
		public VehicleService(AppDbContext dbContext) : base(dbContext)
		{ }

	}
}
