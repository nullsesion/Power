using Newtonsoft.Json;

namespace Power.WebApi.DomainModel
{
	public record Current
	{
		public string Region { get; set; }
		public string Date { get; set; }
		public decimal TempC { get; set; }
		public decimal WindKph { get; set; }
		public int Humidity { get; set; }
		public string ConditionText { get; set; }
		public string Icon { get; set; }
	}
}
