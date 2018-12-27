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

		public bool ChangeRole(UserJsonModel model)
		{
			var user = DbContext.Users
								.Include(u => u.UserRoles)
								.SingleOrDefault(c => c.Id == model.Id);

			if (user != null)
			{
				user.UserRoles.Clear();
				if (model.IsAdmin)
				{
					var adminRole = DbContext.Roles.SingleOrDefault(r => r.Name == UserRoles.Admin.ToString());
					var userRole = new UserRole { UserId = user.Id, RoleId = adminRole.Id };

					DbContext.UserRoles.Add(userRole);
				}
				DbContext.SaveChanges();
				return true;
			}
			return false;
		}

		public IEnumerable<UserJsonModel> GetUsers(string search)
		{
			var query = DbContext.Users
								 .Include(u => u.UserRoles)
								 .AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(u => u.Email.Contains(search) ||
									     u.UserName.Contains(search));
			}

			return query.OrderByDescending(u => u.UserRoles.Count)
						.ThenBy(u => u.Email)
						.Select(u => (UserJsonModel)this.Map(u, new UserJsonModel
						{
							IsAdmin = u.UserRoles.Count > 0
						}));
		}
	}
}
