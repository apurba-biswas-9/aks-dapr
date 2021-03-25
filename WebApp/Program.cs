using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel;
using System.IO;
using Microsoft.AspNetCore;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var host = BuildWebHost(configuration, args);
            host.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            return builder.Build();
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .CaptureStartupErrors(false)
               .ConfigureKestrel(options =>
               {
                   var httpPort = configuration.GetValue("PORT", 80);
                   options.Listen(IPAddress.Any, httpPort, listenOptions =>
                   {
                       listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                   });
               })
               .UseStartup<Startup>()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseConfiguration(configuration)
               .Build();
    }
}
