using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public class ForecastResponse
	{
		[JsonProperty("location")]
		public Location Location { get; set; }

		[JsonProperty("current")]
		public Current Current { get; set; }

		[JsonProperty("forecast")]
		public Forecast Forecast { get; set; }
	}
}
