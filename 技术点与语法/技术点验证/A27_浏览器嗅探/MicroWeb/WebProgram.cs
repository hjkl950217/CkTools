using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace 技术点验证
{
    public static class WebProgram
    {
        public static IHost CreateHost()
        {
            return CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://localhost:5000");
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(InitConfig)
                .ConfigureLogging((context, log) =>
                {
                    log.AddConsole();
                });
        }

        private static void InitConfig(IConfigurationBuilder builder)
        {
            // builder.AddJsonFile($"appsettings.json", false, false);
        }
    }
}