using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record DayDataDTO
	{
		[JsonProperty("maxtemp_c")]
		public decimal MaxTempC { get; set; }

		[JsonProperty("maxtemp_f")]
		public decimal MaxTempF { get; set; }

		[JsonProperty("mintemp_c")]
		public decimal MinTempC { get; set; }

		[JsonProperty("mintemp_f")]
		public decimal MinTempF { get; set; }

		[JsonProperty("avgtemp_c")]
		public decimal AvgTempC { get; set; }

		[JsonProperty("avgtemp_f")]
		public decimal AvgTempF { get; set; }

		[JsonProperty("maxwind_kph")]
		public decimal MaxWindKph { get; set; }

		[JsonProperty("totalprecip_mm")]
		public decimal TotalPrecipMm { get; set; }

		[JsonProperty("avgvis_km")]
		public decimal AvgVisKm { get; set; }

		[JsonProperty("avghumidity")]
		public int AvgHumidity { get; set; }

		[JsonProperty("daily_will_it_rain")]
		public int DailyWillItRain { get; set; }

		[JsonProperty("daily_chance_of_rain")]
		public int DailyChanceOfRain { get; set; }

		[JsonProperty("daily_will_it_snow")]
		public int DailyWillItSnow { get; set; }

		[JsonProperty("daily_chance_of_snow")]
		public int DailyChanceOfSnow { get; set; }

		[JsonProperty("condition")]
		public ConditionDTO Condition { get; set; }

		[JsonProperty("uv")]
		public decimal Uv { get; set; }
	}
}
