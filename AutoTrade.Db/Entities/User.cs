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
		public Towns TownId { get; set; }
		public string Address { get; set; }

		public virtual ICollection<Vehicle> Vehicles { get; set; }

		public User()
		{
			this.Vehicles = new HashSet<Vehicle>();
		}
	}
}
