using System.Collections.Generic;

namespace AutoTrade.Core.JsonModels
{
	public class ResponseJsonModel
	{
		public bool Succeeded { get; set; }
		public ICollection<ErrorJsonModel> Errors { get; set; } = new List<ErrorJsonModel>();
		public object Data { get; set; }
	}


	public class ErrorJsonModel
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}
}
