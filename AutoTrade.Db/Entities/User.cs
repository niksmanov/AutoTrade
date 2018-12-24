using AutoTrade.Db.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class User : IdentityUser
	{
		public string Address { get; set; }

		public int? TownId { get; set; }
		public virtual Town Town { get; set; }

		public virtual ICollection<Vehicle> Vehicles { get; set; }

		public User()
		{
			this.Vehicles = new HashSet<Vehicle>();
		}
	}
}
