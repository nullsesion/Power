using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record WeatherResponse
	{
		[JsonProperty("location")]
		public Location Location { get; set; }

		[JsonProperty("current")]
		public Current Current { get; set; }
	}
}
