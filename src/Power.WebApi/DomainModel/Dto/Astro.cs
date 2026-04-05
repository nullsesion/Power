using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record Astro
	{
		[JsonProperty("sunrise")]
		public string Sunrise { get; set; }

		[JsonProperty("sunset")]
		public string Sunset { get; set; }

		[JsonProperty("moonrise")]
		public string Moonrise { get; set; }

		[JsonProperty("moonset")]
		public string Moonset { get; set; }

		[JsonProperty("moon_phase")]
		public string MoonPhase { get; set; }
	}
}
