namespace Power.WebApi.DomainModel
{
	public record Weather
	{
		public string ConditionText { get; }

		public string ConditionIconUrl { get; }

		public int ConditionCode { get; }

		public DateTime? LastUpdated { get; }

		public decimal TempC { get; }
		public decimal TempF { get; }

		public string WindDir { get; }
		public decimal WindMph { get; }
		public decimal WindKph { get; }
		public decimal PressureMb { get; }
		public decimal PrecipMm { get; }
		private Weather(dynamic jsonData, DateTime dateTime)
		{
			ConditionText = jsonData.condition.text;
			ConditionIconUrl = jsonData.condition.icon;
			ConditionCode = jsonData.condition.code;
			LastUpdated = dateTime;     // jsonData.last_updated
			TempC = jsonData.temp_c;
			TempF = jsonData.temp_f;
			WindDir = jsonData.wind_dir;        // string Направление ветра в 16 - точечном компасе(например: NNW).
			WindMph = jsonData.wind_mph;        // decimal Скорость ветра в милях в час.
			WindKph = jsonData.wind_kph;        // decimal Скорость ветра в километрах в час.
			PressureMb = jsonData.pressure_mb;  // decimal Атмосферное давление в миллиметрах.
			PrecipMm = jsonData.precip_mm;      // decimal Количество осадков в миллиметрах.
		}
		public static Result<Weather> Create(dynamic jsonData, DateTime? dateTime = null)
		{
			try
			{
				Weather result;
				if (dateTime is null)
				{
					result = new Weather(jsonData, (DateTime)jsonData.last_updated);
				}
				else
				{
					result = new Weather(jsonData, (DateTime)dateTime);
				}
				return result;
			}
			catch (Exception ex)
			{
				return Result.Failure<Weather>("error parse json");
			}
		}
	}
}
