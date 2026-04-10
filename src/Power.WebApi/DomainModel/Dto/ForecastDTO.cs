using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record ForecastDTO
	{
		[JsonProperty("forecastday")]
		public List<ForecastDayDTO> ForecastDays { get; set; }
	}
}
