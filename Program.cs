using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Windows.Forms;
using winamptospotifyforms.Service;

namespace winamptospotifyforms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using var log = new LoggerConfiguration()
                                .WriteTo.File("./logs.txt")
                                .CreateLogger();
            log.Information("Done setting up serilog!");
            var host = CreateHostBuilder(log).Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<WinampToSpotify>());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder(ILogger logger)
        {
            
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ISpotifyService, SpotifyService>();
                    services.AddTransient<WinampToSpotify>();
                    services.AddSingleton(logger);
                });
        }
    }

}
