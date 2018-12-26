using AutoTrade.Core.JsonModels;
using AutoTrade.Db;
using AutoTrade.Db.Entities;
using AutoTrade.Db.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AutoTrade.Services.UserService
{
	public class UserService : BaseService, IUserService
	{
		public UserService(AppDbContext dbContext) : base(dbContext)
		{ }

		public UserJsonModel GetByEmail(string email)
		{
			var user = DbContext.Users
								.SingleOrDefault(u => u.Email == email);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public UserJsonModel GetById(string id)
		{
			var user = DbContext.Users
								.SingleOrDefault(u => u.Id == id);
			return (UserJsonModel)this.Map(user, new UserJsonModel());
		}

		public bool IsExists(string email)
		{
			var user = DbContext.Users
								.SingleOrDefault(u => u.Email == email);
			return user == null ? false : true;
		}

		public string GetUserName(string email)
		{
			var user = DbContext.Users
								.SingleOrDefault(u => u.Email == email);
			return user != null ? user.UserName : null;
		}

		public bool RemoveUser(string id)
		{
			var user = DbContext.Users.SingleOrDefault(c => c.Id == id);

			if (user != null)
			{
				DbContext.Users.Remove(user);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<UserJsonModel> GetUsers()
		{
			return DbContext.Users
							.OrderBy(u => u.UserRoles.Count)
							.ThenBy(u => u.Email)
							.Select(u => (UserJsonModel)this.Map(u, new UserJsonModel
							{
								IsAdmin = u.UserRoles
										   .SingleOrDefault(x => x.UserId == u.Id)
										   .Role.Name == UserRoles.Admin.ToString()
							}));
		}

		public bool ChangeRole(UserJsonModel model)
		{
			var user = DbContext.Users.SingleOrDefault(c => c.Id == model.Id);

			if (user != null)
			{
				user.UserRoles.Clear();

				string roleName = model.IsAdmin ? UserRoles.Admin.ToString() : UserRoles.User.ToString();
				var role = DbContext.Roles.SingleOrDefault(r => r.Name == roleName);
				var userRole = new UserRole { UserId = user.Id, RoleId = role.Id };

				DbContext.UserRoles.Add(userRole);
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}
	}
}
