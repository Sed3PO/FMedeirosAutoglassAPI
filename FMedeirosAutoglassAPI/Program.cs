using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Net;

namespace FMedeirosAutoglassAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("AplicationName", "FMedeirosAutoglassAPI")
                    .WriteTo.File(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"),
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: LogEventLevel.Verbose)
                    .CreateLogger();

                Log.Information("Application Started");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Main - Fatal Error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}