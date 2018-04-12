using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreTester
{
    public class Program
    {
        public static void Main(string[] args)
        {


            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var appconfig = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(appconfig)
                .ConfigureServices(configureServices =>
                {
                    // configureServices.AddSingleton<IConfigManager, ConfigManager>();
                })
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(config =>
                {
                    //config.SetBasePath(Directory.GetCurrentDirectory());
                    //config.AddJsonFile("Configs/config.json", false, true);
                    //config.AddJsonFile("Configs/json.json", false, true);
                })
                .Build();
        }            
    }
}
