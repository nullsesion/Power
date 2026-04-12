namespace Power.WebApiApp.Models
{
	public class ResopnseWeather
	{
		public WeatherResponseDTO weatherResponseDTO { get; set; }
		public WeatherResponseDTO[] forecastResponseDTO {  get; set; }
	}
}
