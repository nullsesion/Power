namespace Power.WebApi.DomainModel
{
	public class WeatherDay
	{
		public DateTime? Date { get; }
		public string ConditionText { get; }
		public string ConditionIconUrl { get; }
		public int ConditionCode { get; }
		/*

		public decimal TempC { get; }
		public decimal TempF { get; }
		public string WindDir { get; }
		public decimal WindMph { get; }
		public decimal WindKph { get; }
		public decimal? PressureMb { get; }
		public decimal? PrecipMm { get; }
		*/
		private WeatherDay(dynamic jsonData, DateTime dateTime)
		{
			Date = dateTime;
			ConditionText = jsonData.condition.text;
			ConditionIconUrl = jsonData.condition.icon;
			ConditionCode = jsonData.condition.code;
			/*
			TempC = jsonData.temp_c is null ? jsonData.avgtemp_c : jsonData.temp_c;
			TempF = jsonData.temp_f is null ? jsonData.avgtemp_f : jsonData.temp_f;
			WindDir = jsonData.wind_dir ?? "";        // string Направление ветра в 16 - точечном компасе(например: NNW).
			WindMph = jsonData.wind_mph is null ? jsonData.maxwind_mph : jsonData.wind_mph;        // decimal Скорость ветра в милях в час.
			WindKph = jsonData.wind_kph is null ? jsonData.maxwind_kph : jsonData.wind_kph;        // decimal Скорость ветра в километрах в час.
			PressureMb = jsonData?.pressure_mb;  // decimal Атмосферное давление в миллиметрах.
			PrecipMm = jsonData?.precip_mm;      // decimal Количество осадков в миллиметрах.
			*/
		}
		public static Result<WeatherDay> Create(dynamic jsonData, DateTime? dateTime = null)
		{
			try
			{
				WeatherDay result;
				if (dateTime is null)
				{
					result = new WeatherDay(jsonData, (DateTime)jsonData.last_updated);
				}
				else
				{
					result = new WeatherDay(jsonData, (DateTime)dateTime);
				}
				return result;
			}
			catch (Exception ex)
			{
				return Result.Failure<WeatherDay>("error parse json");
			}
		}
	}
}
