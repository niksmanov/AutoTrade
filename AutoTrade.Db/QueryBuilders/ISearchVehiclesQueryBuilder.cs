using System.Linq;
using AutoTrade.Db.Entities;

namespace AutoTrade.Db.QueryBuilders
{
	public interface ISearchVehiclesQueryBuilder
	{
		IQueryable<Vehicle> FilterABS(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterAirbags(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterAirConditioning(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterAutoPilot(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterCentralLocking(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterColor(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterESP(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterFromCubicCapacity(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterFromHorsePower(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterFromPrice(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterFromProductionDate(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterFuelType(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterGearboxType(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterMake(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterModel(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterToCubicCapacity(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterToHorsePower(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterToPrice(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterToProductionDate(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterTown(IQueryable<Vehicle> query);
		IQueryable<Vehicle> FilterType(IQueryable<Vehicle> query);
	}
}