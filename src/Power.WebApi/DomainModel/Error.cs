using Newtonsoft.Json;

namespace Power.WebApi.DomainModel
{
	public record Error(string Message
		, ErrorType Type
		)
	{
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		static public Error? Deserialize(string value)
		{
			return JsonConvert.DeserializeObject<Error>(value);
		}
	};
}
