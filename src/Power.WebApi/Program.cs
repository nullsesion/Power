using Microsoft.Net.Http.Headers;
using Polly;
using Power.WebApi.Services;
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

			builder.Services.AddHttpClient<WeatherApiClientService>(client =>
			{
				IConfigurationSection section = builder.Configuration.GetSection(nameof(WeatherApiClientService));
				string host = section.GetValue<string>("Host");
				client.BaseAddress = new Uri(host);
				client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "WebApp");
			})
			
			
			//Microsoft.Extensions.Http.Polly для нестабильных соединений
			.AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(10)))
			;

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
