using Newtonsoft.Json;

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
			_forecast = $"/v1/forecast.json?key={key}&q={q}&lang={lang}&days=2";
		}

		public async Task<Result<Weather, ErrorList>> GetCurrentAsync(CancellationToken cancellationToken)
		{
			ErrorList errorList = new ErrorList();
			Result<dynamic, ErrorList> valueFromUrl = await _TryGetDataFromUrl(_current, cancellationToken);
			if (valueFromUrl.IsSuccess && valueFromUrl.Value?.current is not null)
			{
				Result<Weather> weather = Weather.Create(valueFromUrl.Value?.current);
				if (weather.IsSuccess)
				{
					return weather.Value;
				}
				errorList.AddError(new Error(weather.Error, ErrorType.ServerError));
			}
			return Result.Failure<Weather, ErrorList>(valueFromUrl.Error);
		}

		public async Task<Result<dynamic, ErrorList>> GetForecastAsync(CancellationToken cancellationToken)
		{
			var valueForecastFromUrl = await _TryGetDataFromUrl(_forecast, cancellationToken);
			if (valueForecastFromUrl.IsSuccess)
			{
				return valueForecastFromUrl.Value;
			}
			return Result.Failure<dynamic, ErrorList>(valueForecastFromUrl.Error);
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
