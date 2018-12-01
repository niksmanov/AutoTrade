using System.Collections.Generic;

namespace AutoTrade.Core.JsonModels
{
	public class ResponseJsonModel
	{
		public ResponseJsonModel(bool succeeded = false, object data = null, string error = null, List<string> errors = null)
		{
			this.Succeeded = succeeded;
			this.Data = data;
			if (errors != null)
				this.Errors = errors;
			if (error != null)
				this.Errors.Add(error);
		}

		public bool Succeeded { get; set; }
		public ICollection<string> Errors { get; set; } = new List<string>();
		public object Data { get; set; }
	}
}
