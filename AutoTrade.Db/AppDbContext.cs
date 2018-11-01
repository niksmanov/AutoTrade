using AutoTrade.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoTrade.Db
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{ }

		public DbSet<User> Users { get; set; }
	}
}
