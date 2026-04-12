using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Power.WebApiApp.Models;
using Power.WebApiApp.Services;
using System.Diagnostics;

namespace Power.WebApiApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration _config;
		private WeatherApiClientService _weatherApiClientService;

		public HomeController(ILogger<HomeController> logger, IConfiguration config, WeatherApiClientService weatherApiClientService)
		{
			_logger = logger;
			_config = config;
			_weatherApiClientService = weatherApiClientService;
		}

		public async Task<IActionResult> Static(CancellationToken cancellationToken)
		{
			ResopnseWeather view = new ResopnseWeather();
			Result<WeatherResponseDTO, ErrorList> currentResult = await _weatherApiClientService.GetCurrentAsync(cancellationToken);
			if (currentResult.IsSuccess)
			{
				view.weatherResponseDTO = currentResult.Value;
			}
			Result<WeatherResponseDTO[], ErrorList> forecastResult = await _weatherApiClientService.GetForecastAsync(cancellationToken);
			if (forecastResult.IsSuccess)
			{
				view.forecastResponseDTO = forecastResult.Value;
			}
			return View(view);

		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
