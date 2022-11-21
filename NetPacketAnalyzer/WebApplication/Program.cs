using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string configPath;
            if (args.Length != 1) {
                configPath = "appsettings.json";
            }
            else
            {
                configPath = args[0];
            }
            Console.WriteLine("START"); 
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(configPath)
               .Build();
            CreateHostBuilder(args, config, configPath).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration, string configPath) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((context, builder) =>
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(configPath)
                    .Build()
                    );
    }
}
