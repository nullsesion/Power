using CSharpFunctionalExtensions;
using Power.WebApiApp.Interfaces;
using Power.WebApiApp.Models;

namespace Power.WebApiApp.Services
{
	public class WeatherApiClientService
	{
		private readonly IWeatherApi _weatherApi;
		private readonly IConfiguration _configuration;

		public WeatherApiClientService(IWeatherApi weatherApi, IConfiguration configuration) => (_weatherApi, _configuration) = (weatherApi, configuration);

		public async Task<Result<WeatherResponseDTO, ErrorList>> GetCurrentAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			try
			{
				WeatherResponseDTO? weatherResponse = await _weatherApi.GetCurrentWeatherAsync(cancellationToken);
				if (weatherResponse is not null)
				{
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

		public async Task<Result<WeatherResponseDTO[], ErrorList>> GetForecastAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			try
			{
				WeatherResponseDTO[]? forecastResponse = await _weatherApi.GetForecastDaysAsync(cancellationToken);
				if (forecastResponse is not null)
				{
					return Result.Success<WeatherResponseDTO[], ErrorList>(forecastResponse);
				}

				errorList.AddError(new Error("Invalid external I/O operation", ErrorType.External));
				return Result.Failure<WeatherResponseDTO[], ErrorList>(errorList);
			}
			catch (OperationCanceledException)
			{
				errorList.AddError(new Error("Client Closed Request", ErrorType.Conflict));
			}
			catch (Exception)
			{
				errorList.AddError(new Error("Not correct parsing or external I/O operation", ErrorType.External));
			}
			return Result.Failure<WeatherResponseDTO[], ErrorList>(errorList);
		}
	}
}
