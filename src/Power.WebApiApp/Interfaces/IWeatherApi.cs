using Refit;
using Power.WebApiApp.Models;

namespace Power.WebApiApp.Interfaces
{
	public interface IWeatherApi
	{
		[Get("/api/v1/Weather/current.json")]
		Task<WeatherResponseDTO> GetCurrentWeatherAsync(CancellationToken cancellationToken = default);

		[Get("/api/v1/Weather/forecast.json")]
		//ICurrent[] Days
		Task<WeatherResponseDTO[]> GetForecastDaysAsync(CancellationToken cancellationToken = default);
	
	}
}
