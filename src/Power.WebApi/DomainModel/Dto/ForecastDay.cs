using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record ForecastDay
	{
		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("date_epoch")]
		public long DateEpoch { get; set; }

		[JsonProperty("day")]
		public DayData Day { get; set; }

		[JsonProperty("astro")]
		public Astro Astro { get; set; }

		[JsonProperty("hour")]
		public List<HourData> Hour { get; set; }
	}
}
