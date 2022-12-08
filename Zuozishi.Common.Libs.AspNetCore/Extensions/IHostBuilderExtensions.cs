using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zuozishi.Common.Libs.AspNetCore.Utils;

namespace Zuozishi.Common.Libs.AspNetCore.Extensions;

public static class IHostBuilderExtensions
{
	public static IHostBuilder UseSerilogLogger(this IHostBuilder builder)
	{
		var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.AddJsonFile($"logger.{env}.json", true, true).Build();
		Log.Logger = new LoggerConfiguration()
			.Enrich.WithProperty("Environment", env)
			.Enrich.With(new MiniSourceContextEnricher())
			.ReadFrom.Configuration(configuration)
			.CreateLogger();
		builder.UseSerilog();
		return builder;
	}
}
