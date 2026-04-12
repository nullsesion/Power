using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record LocationDTO
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("lat")]
		public double Lat { get; set; }

		[JsonProperty("lon")]
		public double Lon { get; set; }

		[JsonProperty("localtime")]
		public DateTime LocalTime { get; set; }
	}
}
