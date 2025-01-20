using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Windows.Forms;
using WinamptoSpotifyWinForms.Service;

namespace WinamptoSpotifyWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            var host = CreateHostBuilder().Build();         
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<WinampToSpotify>());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseSerilog((hostContext, services, configuration) => {
                    configuration.WriteTo.File("./logs.txt");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ISpotifyService, SpotifyService>();
                    services.AddTransient<WinampToSpotify>();                   
                });
        }
    }

}
