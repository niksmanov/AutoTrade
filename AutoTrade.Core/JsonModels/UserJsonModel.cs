using System.ComponentModel.DataAnnotations;

namespace AutoTrade.Core.JsonModels
{
	public class UserJsonModel
	{
		public string Id { get; set; }
		[Required]
		public string Email { get; set; }
		public string Password { get; set; }
		public string Code { get; set; }
		public string UserName { get; set; }
		public bool RememberMe { get; set; }
		public bool IsAdmin { get; set; }
	}
}
