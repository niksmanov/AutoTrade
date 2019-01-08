using AutoTrade.Core.JsonModels;
using AutoTrade.Db.Entities;
using System.Linq;

namespace AutoTrade.Db.QueryBuilders
{
	public class SearchVehiclesQueryBuilder : ISearchVehiclesQueryBuilder
	{
		private readonly SearchVehiclesJsonModel _search;

		public SearchVehiclesQueryBuilder(SearchVehiclesJsonModel search)
		{
			_search = search;
		}

		public IQueryable<Vehicle> FilterTown(IQueryable<Vehicle> query)
		{
			if (_search.TownId.HasValue)
				return query.Where(v => v.User.TownId == _search.TownId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterMake(IQueryable<Vehicle> query)
		{
			if (_search.MakeId.HasValue)
				return query.Where(v => v.MakeId == _search.MakeId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterModel(IQueryable<Vehicle> query)
		{
			if (_search.ModelId.HasValue)
				return query.Where(v => v.ModelId == _search.ModelId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterColor(IQueryable<Vehicle> query)
		{
			if (_search.ColorId.HasValue)
				return query.Where(v => v.ColorId == _search.ColorId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterType(IQueryable<Vehicle> query)
		{
			if (_search.TypeId.HasValue)
				return query.Where(v => v.TypeId == _search.TypeId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterFuelType(IQueryable<Vehicle> query)
		{
			if (_search.FuelTypeId.HasValue)
				return query.Where(v => v.FuelTypeId == _search.FuelTypeId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterGearboxType(IQueryable<Vehicle> query)
		{
			if (_search.GearboxTypeId.HasValue)
				return query.Where(v => v.GearboxTypeId == _search.GearboxTypeId.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterAirbags(IQueryable<Vehicle> query)
		{
			if (_search.Airbags.HasValue)
				return query.Where(v => v.Airbags == _search.Airbags.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterABS(IQueryable<Vehicle> query)
		{
			if (_search.ABS.HasValue)
				return query.Where(v => v.ABS == _search.ABS.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterESP(IQueryable<Vehicle> query)
		{
			if (_search.ESP.HasValue)
				return query.Where(v => v.ESP == _search.ESP.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterCentralLocking(IQueryable<Vehicle> query)
		{
			if (_search.CentralLocking.HasValue)
				return query.Where(v => v.CentralLocking == _search.CentralLocking.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterAirConditioning(IQueryable<Vehicle> query)
		{
			if (_search.AirConditioning.HasValue)
				return query.Where(v => v.AirConditioning == _search.AirConditioning.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterAutoPilot(IQueryable<Vehicle> query)
		{
			if (_search.AutoPilot.HasValue)
				return query.Where(v => v.AutoPilot == _search.AutoPilot.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterFromCubicCapacity(IQueryable<Vehicle> query)
		{
			if (_search.FromCubicCapacity.HasValue)
				return query.Where(v => v.CubicCapacity >= _search.FromCubicCapacity.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterToCubicCapacity(IQueryable<Vehicle> query)
		{
			if (_search.ToCubicCapacity.HasValue)
				return query.Where(v => v.CubicCapacity <= _search.ToCubicCapacity.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterFromHorsePower(IQueryable<Vehicle> query)
		{
			if (_search.FromHorsePower.HasValue)
				return query.Where(v => v.HorsePower >= _search.FromHorsePower.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterToHorsePower(IQueryable<Vehicle> query)
		{
			if (_search.ToHorsePower.HasValue)
				return query.Where(v => v.HorsePower <= _search.ToHorsePower.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterFromPrice(IQueryable<Vehicle> query)
		{
			if (_search.FromPrice.HasValue)
				return query.Where(v => v.Price >= _search.FromPrice.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterToPrice(IQueryable<Vehicle> query)
		{
			if (_search.ToPrice.HasValue)
				return query.Where(v => v.Price <= _search.ToPrice.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterFromProductionDate(IQueryable<Vehicle> query)
		{
			if (_search.FromProductionDate.HasValue)
				return query.Where(v => v.ProductionDate >= _search.FromProductionDate.Value);
			return query;
		}

		public IQueryable<Vehicle> FilterToProductionDate(IQueryable<Vehicle> query)
		{
			if (_search.ToProductionDate.HasValue)
				return query.Where(v => v.ProductionDate <= _search.ToProductionDate.Value);
			return query;
		}
	}
}
