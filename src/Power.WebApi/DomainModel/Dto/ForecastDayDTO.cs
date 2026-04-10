using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record ForecastDayDTO
	{
		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("date_epoch")]
		public long DateEpoch { get; set; }

		[JsonProperty("day")]
		public DayDataDTO Day { get; set; }

		[JsonProperty("astro")]
		public AstroDTO Astro { get; set; }

		[JsonProperty("hour")]
		public List<HourDataDTO> Hour { get; set; }
	}
}
