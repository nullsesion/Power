using Power.Interfaces;

namespace Power.WebApiApp.Models
{
	public class ForecastResponseDTO : IForecast
	{
		public ICurrent[] Days { get; set; }
	}
}
