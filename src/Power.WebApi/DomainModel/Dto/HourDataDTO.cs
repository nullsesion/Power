using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record HourDataDTO
	{
		[JsonProperty("time")]
		public string Time { get; set; }

		[JsonProperty("temp_c")]
		public double TempC { get; set; }

		[JsonProperty("temp_f")]
		public double TempF { get; set; }

		[JsonProperty("condition")]
		public ConditionDTO Condition { get; set; }

		[JsonProperty("wind_kph")]
		public double WindKph { get; set; }

		[JsonProperty("humidity")]
		public int Humidity { get; set; }

		[JsonProperty("chance_of_rain")]
		public int ChanceOfRain { get; set; }

		[JsonProperty("chance_of_snow")]
		public int ChanceOfSnow { get; set; }

		[JsonProperty("feelslike_c")]
		public double FeelsLikeC { get; set; }
	}
}
