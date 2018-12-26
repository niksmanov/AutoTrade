using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class Role : IdentityRole
	{
		public virtual ICollection<UserRole> UserRoles { get; set; }

		public Role()
		{
			this.UserRoles = new HashSet<UserRole>();
		}
	}
}
