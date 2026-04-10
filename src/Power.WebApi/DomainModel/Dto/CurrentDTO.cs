using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public class CurrentDTO
	{
		
		[JsonProperty("temp_c")]
		public decimal TempC { get; set; }

		[JsonProperty("temp_f")]
		public decimal TempF { get; set; }

		[JsonProperty("wind_mph")]
		public decimal WindMph { get; set; }

		[JsonProperty("wind_kph")]
		public decimal WindKph { get; set; }

		[JsonProperty("cloud")]
		public int Cloud { get; set; }

		[JsonProperty("feelslike_c")]
		public decimal FeelsLikeC { get; set; }

		[JsonProperty("feelslike_f")]
		public decimal FeelsLikeF { get; set; }

		[JsonProperty("humidity")]
		public int Humidity { get; set; }

		[JsonProperty("pressure_mb")]
		public decimal PressureMb { get; set; }

		[JsonProperty("condition")]
		public ConditionDTO Condition { get; set; }

		[JsonProperty("uv")]
		public decimal Uv { get; set; }
	}
}
