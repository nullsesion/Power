using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public class ForecastResponseDTO
	{
		[JsonProperty("location")]
		public LocationDTO Location { get; set; }

		[JsonProperty("current")]
		public CurrentDTO Current { get; set; }

		[JsonProperty("forecast")]
		public ForecastDTO Forecast { get; set; }
	}
}
