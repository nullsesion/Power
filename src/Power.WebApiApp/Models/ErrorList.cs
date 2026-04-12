using Newtonsoft.Json;

namespace Power.WebApiApp.Models
{
	public class ErrorList
	{
		private readonly List<Error> _errors = new List<Error>();
		public bool IsError { get; private set; } = false;
		public IReadOnlyList<Error> Errors => _errors;

		public void AddError(Error error)
		{
			IsError = true;
			_errors.Add(error);
		}
		public void AddErrors(IEnumerable<Error> errors)
		{
			IsError = true;
			_errors.Concat(errors);
		}
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public Error? Deserialize(string value)
		{
			return JsonConvert.DeserializeObject<Error>(value);
		}
	}
}
