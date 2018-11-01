using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade.Db.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public byte[] Hash { get; set; }
	}
}
