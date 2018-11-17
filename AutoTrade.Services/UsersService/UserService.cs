using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using System.Linq;

namespace AutoTrade.Services.UsersService
{
	public class UserService : BaseService, IUserService
	{
		public UserService(AppDbContext dbContext) : base(dbContext)
		{ }

		public UserJsonModel GetByEmail(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public UserJsonModel GetById(string id)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Id == id);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public bool IsExists(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return user == null ? false : true;
		}

		public string GetUserName(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return user != null ? user.UserName : null;
		}
	}
}
