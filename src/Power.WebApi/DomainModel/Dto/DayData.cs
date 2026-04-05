using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record DayData
	{
		[JsonProperty("maxtemp_c")]
		public double MaxTempC { get; set; }

		[JsonProperty("maxtemp_f")]
		public double MaxTempF { get; set; }

		[JsonProperty("mintemp_c")]
		public double MinTempC { get; set; }

		[JsonProperty("mintemp_f")]
		public double MinTempF { get; set; }

		[JsonProperty("avgtemp_c")]
		public double AvgTempC { get; set; }

		[JsonProperty("avgtemp_f")]
		public double AvgTempF { get; set; }

		[JsonProperty("maxwind_kph")]
		public double MaxWindKph { get; set; }

		[JsonProperty("totalprecip_mm")]
		public double TotalPrecipMm { get; set; }

		[JsonProperty("avgvis_km")]
		public double AvgVisKm { get; set; }

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
		public Condition Condition { get; set; }

		[JsonProperty("uv")]
		public double Uv { get; set; }
	}
}
