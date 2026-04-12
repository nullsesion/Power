using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Power.WebApiApp.Interfaces;
using Power.WebApiApp.Services;
using Refit;

namespace Power.WebApiApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			//добавить refit настроить его
			var settings = new RefitSettings
			{
				ContentSerializer = new NewtonsoftJsonContentSerializer(
					new JsonSerializerSettings
					{
						ContractResolver = new DefaultContractResolver
						{
							NamingStrategy = new CamelCaseNamingStrategy()
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

			builder.Services.AddScoped<WeatherApiClientService>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
