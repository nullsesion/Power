using Power.Interfaces;

namespace Power.WebApi.DomainModel
{
	public class Forecast : IForecast
	{
		//public Current[] Days { get; set; }
		ICurrent[] IForecast.Days { get; set; }
	}
}
 