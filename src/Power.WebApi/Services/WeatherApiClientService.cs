using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Power.WebApi.DomainModel;
using System;


namespace Power.WebApi.Services
{
	public class WeatherApiClientService
	{
		private readonly string _host;
		private readonly string _current;
		private readonly string _forecast;

		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public WeatherApiClientService(IHttpClientFactory clientFactory, IConfiguration configuration)
		{
			_httpClient = clientFactory.CreateClient(nameof(WeatherApiClientService));
			_configuration = configuration;
			var section = _configuration.GetSection(nameof(WeatherApiClientService));

			_host = section.GetValue<string>("Host") ?? "";
			var q = section.GetValue<string>("q");
			var days = section.GetValue<int>("days");
			var lang = section.GetValue<string>("lang");

			string key = section.GetValue<string>("api_key");
			_current = $"/v1/current.json?key={key}&q={q}&days={days}&lang={lang}";
			_forecast = $"/v1/forecast.json?key={key}&q={q}&lang={lang}&days={days}";
		}

		public async Task<Result<Weather, ErrorList>> GetCurrentAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync(_current, cancellationToken);
				if (response.IsSuccessStatusCode)
				{
					string jsonResponse = await response.Content.ReadAsStringAsync();
					dynamic result = JsonConvert.DeserializeObject(jsonResponse);
					if (result?.current is not null)
					{
						Result<Weather> weather = Weather.Create(result?.current);
						if (weather.IsSuccess)
						{
							return weather.Value;
						}
						else
						{
							errorList.AddError(new Error(weather.Error, ErrorType.ServerError));
							return Result.Failure<Weather, ErrorList>(errorList);
						}
					}
					return Result.Success<Weather, ErrorList>(result);
				}
				errorList.AddError(new Error("Invalid external I/O operation", ErrorType.External));
				return Result.Failure<Weather, ErrorList>(errorList);
			}
			catch (Exception)
			{
				errorList.AddError(new Error("Not correct parsing or external I/O operation", ErrorType.External));
				return Result.Failure<Weather, ErrorList>(errorList);
			}
		}

		public async Task<Result<WeatherDay[], ErrorList>> GetForecastAsync(CancellationToken cancellationToken)
		{
			Result<dynamic, ErrorList> valueForecastFromUrl = await _TryGetDataFromUrl(_forecast, cancellationToken);
			if (valueForecastFromUrl.IsSuccess)
			{
				List<WeatherDay> result = new List<WeatherDay>();
				var dayCollection = valueForecastFromUrl.Value?.forecast?.forecastday;
				foreach(var day in dayCollection)
				{
					//todo: исправить
					dynamic w = day.day;
					DateTime wd = (DateTime)day.date;
					Result<WeatherDay> d = WeatherDay.Create(w, wd);
					if (d.IsSuccess)
					{
						result.Add(d.Value);
					}
				}
				return valueForecastFromUrl.Value;
			}
			return Result.Failure<WeatherDay[], ErrorList>(valueForecastFromUrl.Error);
		}

		
		private async Task<Result<dynamic, ErrorList>> _TryGetDataFromUrl(string uri, CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync(uri, cancellationToken);
				if (response.IsSuccessStatusCode)
				{
					string jsonResponse = await response.Content.ReadAsStringAsync();
					dynamic result = JsonConvert.DeserializeObject(jsonResponse);
					return Result.Success<dynamic, ErrorList>(result);
				}
				errorList.AddError(new Error("Invalid external I/O operation", ErrorType.External));
				return Result.Failure<dynamic, ErrorList>(errorList);
			}
			catch (Exception)
			{
				errorList.AddError(new Error("Not correct parsing or external I/O operation", ErrorType.External));
				return Result.Failure<dynamic, ErrorList>(errorList);
			}
		}
		
	}
}
