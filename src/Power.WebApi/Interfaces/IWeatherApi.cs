using Power.WebApi.DomainModel.Dto;
using Refit;

namespace Power.WebApi.Interfaces
{
	public interface IWeatherApi
	{
		[Get("/v1/current.json")]
		Task<WeatherResponseDTO> GetCurrentWeatherAsync(
				[AliasAs("key")] string apiKey
				, [AliasAs("q")] string query
				, [AliasAs("lang")] string lang
				, CancellationToken cancellationToken = default
			);
				
		[Get("/v1/forecast.json")]
		Task<ForecastResponseDTO> GetForecastAsync(
			[AliasAs("key")] string apiKey
			, [AliasAs("q")] string query
			, [AliasAs("days")] int days
			, [AliasAs("lang")] string lang = "ru"
			, CancellationToken cancellationToken = default);
	}
}
