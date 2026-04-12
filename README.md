# Погодное веб-приложение на .Net Framework, 

Включает разделение на API и веб приложение. Веб приложение используюет как ajax так и прямые обращения `из под капота`(использоан refit для строгой типизации)
 - Отображение шаблонов jQuery + jsRender
 - Маппинг Mapster (Automaper возможно станет платным)
 - Добавлена возможность отмены запроса (CancellationToken)
 - Работа с api.weatherapi.com через отдельный сервис (шина здесь избыточна по этому http)
 - Использоваты Nuget ```Refit``` - строгая типизация HttpClient, ```CSharpFunctionalExtensions``` - паттерн Result, ```Mapster``` - мапинг

## Как запустить
привести в соответствие appsettings.Development.json сборки Power.WebApi
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "WeatherApiClientService": {
    "Host": "http://api.weatherapi.com",
    "q": "55.45,37.37",
    "days": 3,
    "lang": "ru",
    "api_key": "ваш ключ от api.weatherapi.com"
  },
  "WebProxy": {
    "Address": "ваш прокси сервер",
    "User": "user от прокси",
    "Password": "пароль от прокси"
  }
}
```
WebProxy можно опустить или Fiddler по желанию

![как запустить](Docs/assets/config.jpg)

перед запуском убедится что запускаются две сборки
![как запустить](Docs/assets/howtorun.jpg)
(Power.slnLaunch.user)
```
[
  {
    "Name": "New Profile",
    "Projects": [
      {
        "Path": "src\\Power.WebApi\\Power.WebApi.csproj",
        "Action": "Start"
      },
      {
        "Path": "src\\Power.WebApiApp\\Power.WebApiApp.csproj",
        "Action": "Start"
      }
    ]
  }
]
```

UI
•   Отобразить один экран с погодной информацией: текущая, почасовая (показывать оставшиеся часы из текущего дня и все часы следующего), прогноз погоды на 3 дня.
•   Обработать показ загрузки и ошибку, если что-то пошло не так, с кнопкой повторного запроса
•   По дизайну никаких ограничений нет, все на ваш вкус.
Геолокация и запросы
•   Геолокацию зафиксировать на использование города Москва
•   Данные получать из запросов API:
http://api.weatherapi.com/v1/current.json?key=fa8b3df74d4042b9aa7135114252304&amp;q=LAT,LON
http://api.weatherapi.com/v1/forecast.json?key=fa8b3df74d4042b9aa7135114252304&amp;q=LAT,LON&amp;days=3
Реализация графической составляющей на усмотрения кандидата, оформление должно быть понятным, не запутанным, но соответствовать тз.