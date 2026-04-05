using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record Forecast
	{
		[JsonProperty("forecastday")]
		public List<ForecastDay> ForecastDays { get; set; }
	}
}
