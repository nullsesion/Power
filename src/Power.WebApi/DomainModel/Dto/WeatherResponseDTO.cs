using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record WeatherResponseDTO
	{
		[JsonProperty("location")]
		public LocationDTO Location { get; set; }

		[JsonProperty("current")]
		public CurrentDTO Current { get; set; }
	}
}
