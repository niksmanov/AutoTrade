using AutoTrade.Db;
using System;

namespace AutoTrade.Services
{
	public abstract class BaseService : IDisposable
	{
		public BaseService(AppDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public AppDbContext DbContext { get; private set; }

		public object Map(object sourceObject, object destinationObject)
		{
			if (sourceObject != null && destinationObject != null)
			{
				foreach (var sourceProperty in sourceObject.GetType().GetProperties())
				{
					var destinationProperty = destinationObject.GetType().GetProperty(sourceProperty.Name);
					if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
					{
						destinationProperty.SetValue(destinationObject, sourceProperty.GetValue(sourceObject));
					}
				}
				return destinationObject;
			}
			return null;
		}

		public void Dispose()
		{
			if (this.DbContext != null)
				this.DbContext.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
