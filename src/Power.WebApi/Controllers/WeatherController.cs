using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Power.Interfaces;
using Power.WebApi.DomainModel;
using Power.WebApi.DomainModel.Dto;
using Power.WebApi.Services;
using System.Linq;
using IResult = Microsoft.AspNetCore.Http.IResult;


namespace Power.WebApi.Controllers
{
	[ApiController]
	[Route("/api/v1/[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly ILogger<WeatherController> _logger;
		private readonly WeatherApiClientService _clientWeatherService;
		private readonly IMapper _mapper;

		public WeatherController(ILogger<WeatherController> logger, IMapper mapper, WeatherApiClientService clientWeatherService)
		{
			_mapper = mapper;
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
			Result<WeatherResponseDTO, ErrorList> current = await _clientWeatherService.GetCurrentAsync(cancellationToken);
			if (current.IsSuccess)
			{
				Current currentValue = _mapper.Map<Current>(current.Value);
				return Results.Json(currentValue);
			}
			else
			{
				return Results.BadRequest(current.Error);
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
			Result<ForecastResponseDTO, ErrorList> forecast = await _clientWeatherService.GetForecastAsync(cancellationToken);
			if (forecast.IsSuccess)
			{
				string region = forecast.Value?.Location?.Region ?? "";
				
				Current[] result = forecast.Value.Forecast.ForecastDays.Select(x =>
				{
					return _mapper
					.From(x)
					.AddParameters("Region", region)
					.AdaptToType<Current>()
					; 
				}).ToArray();
								
				return Results.Json(result);
			}
			else
			{
				return Results.BadRequest(forecast.Error);
			}
		}

		/// <summary>
		/// Получение прогноза по часам
		/// </summary>
		/// <param name="cancellationToken">используем cancellationToken для зависщих соединений</param>
		/// <returns></returns>
		[HttpGet("hours.json")]
		public async Task<IResult> GetHoursAsync(CancellationToken cancellationToken)
		{
			Result<ForecastResponseDTO, ErrorList> forecast = await _clientWeatherService.GetHoursAsync(cancellationToken);
			if (forecast.IsSuccess)
			{
				string region = forecast.Value?.Location?.Region ?? "";
				var result = forecast.Value?.Forecast?.ForecastDays?.First()?.Hour?.Select(x => _mapper.Map<Hour>(x))?.ToArray();
				return Results.Json(result);
			}
			else
			{
				return Results.BadRequest(forecast.Error);
			}
		}
	}
}
