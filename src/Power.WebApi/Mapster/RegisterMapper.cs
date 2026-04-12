using Mapster;
using Power.WebApi.DomainModel;
using Power.WebApi.DomainModel.Dto;

namespace Power.WebApi.Mapster
{
	public class RegisterMapper : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.ForType<WeatherResponseDTO, Current>()
				.Map(current => current.Region, weatherResponseDTO => weatherResponseDTO.Location.Region)
				.Map(current => current.Date, weatherResponseDTO => DateOnly.FromDateTime(weatherResponseDTO.Location.LocalTime).ToString("dd.MM.yyy"))
				.Map(current => current.TempC, weatherResponseDTO => weatherResponseDTO.Current.TempC)
				.Map(current => current.WindKph, weatherResponseDTO => weatherResponseDTO.Current.WindKph)
				.Map(current => current.Humidity, weatherResponseDTO => weatherResponseDTO.Current.Humidity)
				.Map(current => current.Icon, weatherResponseDTO => weatherResponseDTO.Current.Condition.Icon)
				.Map(current => current.ConditionText, weatherResponseDTO => weatherResponseDTO.Current.Condition.Text)
				.RequireDestinationMemberSource(true);

			config.ForType<ForecastDayDTO, Current>()
				.Map(current => current.Region, forecastDayDTO => MapContext.Current!.Parameters["Region"])
				.Map(current => current.Date, forecastDayDTO => DateOnly.Parse(forecastDayDTO.Date).ToString("dd.MM.yyy"))
				.Map(current => current.TempC, forecastDayDTO => forecastDayDTO.Day.AvgTempC)
				.Map(current => current.WindKph, forecastDayDTO => forecastDayDTO.Day.AvgHumidity)
				.Map(current => current.Humidity, forecastDayDTO => forecastDayDTO.Day.AvgHumidity)
				.Map(current => current.ConditionText, forecastDayDTO => forecastDayDTO.Day.Condition.Text)
				.Map(current => current.Icon, forecastDayDTO => forecastDayDTO.Day.Condition.Icon)
				.RequireDestinationMemberSource(true);

			config.ForType<HourDataDTO, Hour>()
				.Map(current => current.Time, forecastDayDTO =>DateTime.Parse(forecastDayDTO.Time).ToString("HH.mm"))
				.Map(current => current.TempC, forecastDayDTO => forecastDayDTO.TempC)
				.RequireDestinationMemberSource(true);
			
		}

	}
}
