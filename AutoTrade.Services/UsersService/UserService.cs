using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using System.Linq;

namespace AutoTrade.Services.UsersService
{
	public class UserService : BaseService, IUserService
	{
		public UserService(AppDbContext dbContext) : base(dbContext)
		{ }

		public UserJsonModel GetUserByEmail(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public UserJsonModel GetUserById(string id)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Id == id);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public bool IsUserExists(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return user != null ? true : false;
		}
	}
}
