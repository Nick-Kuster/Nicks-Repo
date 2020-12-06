using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMT5K.Infrastructure;
using Serilog;
using TMT5K.Domain;

namespace TMT5K.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\logs\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration config = hostContext.Configuration;

                    APIInfo apiInfo = config.GetSection("APIInfo").Get<APIInfo>();

                    services.AddSingleton<IAPIInfo>(apiInfo);
                    services.AddTransient<IYoutubeAPI, YouTubeAPI>();
                    services.AddHostedService<Worker>();
                    services.AddHostedService<Worker2>();
                })
                .UseSerilog();
    }
}
