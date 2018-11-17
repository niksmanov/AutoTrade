using System.Collections.Generic;

namespace AutoTrade.Core.JsonModels
{
	public class ResponseJsonModel
	{
		public ResponseJsonModel(bool succeeded = false, object data = null, params string[] errors)
		{
			this.Succeeded = succeeded;
			this.Data = data;
			foreach (string err in errors)
				this.Errors.Add(err);
		}

		public ResponseJsonModel(bool succeeded = false, object data = null, List<string> errors = null)
		{
			this.Succeeded = succeeded;
			this.Data = data;
			this.Errors = errors;
		}

		public bool Succeeded { get; set; }
		public ICollection<string> Errors { get; set; } = new List<string>();
		public object Data { get; set; }
	}
}
