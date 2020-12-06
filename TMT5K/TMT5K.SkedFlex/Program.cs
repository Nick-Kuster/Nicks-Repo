using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMT5K.Domain;
using TMT5K.Infrastructure;
using TMT5K.Service;

namespace TMT5K.SkedFlex
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                });
    }
}
