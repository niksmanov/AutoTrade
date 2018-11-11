using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using System.Linq;

namespace AutoTrade.Services.UsersService
{
	public class UserService : BaseService, IUserService
	{
		private const string INVALID_EMAIL = "Invalid email!";
		private const string EMAIL_EXISTS = "Email already exists!";


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

		public ResponseJsonModel IsUserExists(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			var response = new ResponseJsonModel();
			if (user == null)
			{
				var error = new ErrorJsonModel { Description = INVALID_EMAIL };
				response.Errors.Add(error);
			}
			else
			{
				response.Succeeded = true;
				var error = new ErrorJsonModel { Description = EMAIL_EXISTS };
				response.Errors.Add(error);
			}
			return response;
		}

		public string GetUserUserName(string email)
		{
			var user = DbContext.Users.SingleOrDefault(u => u.Email == email);
			return user != null ? user.UserName : null;
		}
	}
}
