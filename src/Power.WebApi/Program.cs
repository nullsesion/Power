using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Power.WebApi.Interfaces;
using Power.WebApi.Services;
using Refit;
using System.Net;

namespace Power.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//добавить refit настроить его
			var settings = new RefitSettings
			{
				ContentSerializer = new NewtonsoftJsonContentSerializer(
					new JsonSerializerSettings
					{
						ContractResolver = new DefaultContractResolver
						{
							NamingStrategy = new SnakeCaseNamingStrategy()
						}
					})
			};
			IHttpClientBuilder weatherApiClientService = builder.Services
			.AddRefitClient<IWeatherApi>(settings)
			.ConfigureHttpClient(client =>
			{
				IConfigurationSection section = builder.Configuration.GetSection(nameof(WeatherApiClientService));
				string host = section.GetValue<string>("Host");
				client.BaseAddress = new Uri(host);
				client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "WebApp");
			});


			IConfigurationSection section = builder.Configuration.GetSection(nameof(WebProxy));
			string addressWebProxy = section.GetValue<string>("Address");
			string userWebProxy = section.GetValue<string>("User");
			string passwordWebProxy = section.GetValue<string>("Password");

			
			//проверка на наличие настроек прокси
			if (!string.IsNullOrEmpty(addressWebProxy)
				&& !string.IsNullOrEmpty(userWebProxy)
				&& !string.IsNullOrEmpty(passwordWebProxy)
				)
			{
				weatherApiClientService.ConfigurePrimaryHttpMessageHandler(() =>
				{
					HttpClientHandler httpClientHandler = new HttpClientHandler
					{
						Proxy = new WebProxy()
						{
							Address = new Uri(addressWebProxy),
							BypassProxyOnLocal = false,
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential(userWebProxy, passwordWebProxy)
						},
						UseProxy = true
					};
					return httpClientHandler;
				});
			}
			

			//Microsoft.Extensions.Http.Polly для нестабильных соединений
			weatherApiClientService.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(10)));

			builder.Services.AddScoped<WeatherApiClientService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
