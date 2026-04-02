using Microsoft.AspNetCore.Mvc;
using Power.WebApi.Services;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Power.WebApi.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly ILogger<WeatherController> _logger;
		private readonly WeatherApiClientService _clientWeatherService;

		public WeatherController(ILogger<WeatherController> logger, WeatherApiClientService clientWeatherService)
		{
			_logger = logger;
			_clientWeatherService = clientWeatherService;
		}
		/// <summary>
		/// получение текущей погоды
		/// </summary>
		/// <param name="cancellationToken">используем cancellationToken для зависщих соединений</param>
		/// <returns></returns>
		[HttpGet("current.json")]
		public async Task<IResult> Current(CancellationToken cancellationToken)
		{

			Result<Weather, ErrorList> current = await _clientWeatherService.GetCurrentAsync(cancellationToken);
			if (current.IsSuccess)
			{
				return Results.Json(current.Value);
			}
			else
			{
				return Results.BadRequest(new Error("unknown error", ErrorType.ServerError));
			}
		}
		/// <summary>
		/// Получение прогноза
		/// </summary>
		/// <param name="cancellationToken">используем cancellationToken для зависщих соединений</param>
		/// <returns></returns>
		[HttpGet("forecast.json")]
		public async Task<IResult> Forecast(CancellationToken cancellationToken)
		{
			Result<dynamic, ErrorList> forecast = await _clientWeatherService.GetForecastAsync(cancellationToken);
			if (forecast.IsSuccess)
			{
				return forecast.Value;
			}
			else
			{
				return Results.BadRequest(new Error("unknown error", ErrorType.ServerError));
			}
		}
	}
}
