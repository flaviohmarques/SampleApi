using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Filters;
using System;

namespace SampleApi
{
    public class Program
    {
        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                        .WriteTo.Console()
                        .CreateLogger();
            try
            {
                Log.Information("Starting web host, *SampleApi* ");
                CreateHostBuilder().Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var settings = env == null ? "appsettings.Development.json" : $"appsettings.{env}.json";
                    config.AddJsonFile(settings, optional: false, reloadOnChange: true);
                })
                .UseSerilog();
    }
}
