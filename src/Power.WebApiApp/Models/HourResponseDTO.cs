using Power.Interfaces;

namespace Power.WebApiApp.Models
{
	public class HourResponseDTO : IHour
	{
		public string Time { get; set; }
		public decimal TempC { get; set; }
	}
}
