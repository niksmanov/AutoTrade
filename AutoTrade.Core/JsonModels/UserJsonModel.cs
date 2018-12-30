using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Core.JsonModels
{
	public class UserJsonModel
	{
		public string Id { get; set; }
		[Required]
		public string Email { get; set; }
		public string OldPassword { get; set; }
		public string Password { get; set; }
		public string Code { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public int? TownId { get; set; }
		public string TownName { get; set; }
		public bool RememberMe { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool IsAdmin { get; set; }
	}
}
