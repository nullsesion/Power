using Power.WebApi.DomainModel.Dto;
using Power.WebApi.Interfaces;

namespace Power.WebApi.Services
{
	public class WeatherApiClientService
	{
		private readonly IWeatherApi _weatherApi;
		private readonly IConfiguration _configuration;

		private readonly string _key;
		private readonly string _q;
		private readonly int _days;
		private readonly string _lang;

		public WeatherApiClientService(IWeatherApi weatherApi, IConfiguration configuration)
		{
			_weatherApi = weatherApi;
			_configuration = configuration;

			var section = _configuration.GetSection(nameof(WeatherApiClientService));

			_key = section.GetValue<string>("api_key");
			_q = section.GetValue<string>("q");
			_days = section.GetValue<int>("days");
			_lang = section.GetValue<string>("lang");
		}

		public async Task<Result<WeatherResponseDTO, ErrorList>> GetCurrentAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			var section = _configuration.GetSection(nameof(WeatherApiClientService));
			await Task.Delay(5000);
			try
			{
				WeatherResponseDTO weatherResponse = await _weatherApi.GetCurrentWeatherAsync(_key, _q, _lang, cancellationToken);
				if (weatherResponse is not null) {
					return Result.Success<WeatherResponseDTO, ErrorList>(weatherResponse);
				}
				
				errorList.AddError(new Error("Invalid external I/O operation", ErrorType.External));
				return Result.Failure<WeatherResponseDTO, ErrorList>(errorList);
			}
			catch (OperationCanceledException)
			{
				errorList.AddError(new Error("Client Closed Request", ErrorType.Conflict));
			}
			catch (Exception)
			{
				errorList.AddError(new Error("Not correct parsing or external I/O operation", ErrorType.External));
			}
			return Result.Failure<WeatherResponseDTO, ErrorList>(errorList);
		}
		
		public async Task<Result<ForecastResponseDTO, ErrorList>> GetForecastAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			try
			{
				ForecastResponseDTO forecastResponse = await _weatherApi.GetForecastAsync(_key, _q, _days, _lang, cancellationToken);
				if (forecastResponse is not null)
				{
					return Result.Success<ForecastResponseDTO, ErrorList>(forecastResponse);
				}

				errorList.AddError(new Error("Invalid external I/O operation", ErrorType.External));
				return Result.Failure<ForecastResponseDTO, ErrorList>(errorList);
			}
			catch (OperationCanceledException)
			{
				errorList.AddError(new Error("Client Closed Request", ErrorType.Conflict));
			}
			catch (Exception)
			{
				errorList.AddError(new Error("Not correct parsing or external I/O operation", ErrorType.External));
			}
			return Result.Failure<ForecastResponseDTO, ErrorList>(errorList);
		}
	}
}
