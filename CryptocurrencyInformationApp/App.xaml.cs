using CryptocurrencyInformationApp.ViewModels.Main;
using CryptocurrencyInformationApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CryptocurrencyInformationApp
{

    public partial class App : Application
    {
        public static IHost? AppHost { get; set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainView>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<HomeViewModel>();
                    services.AddSingleton<StatisticViewModel>();
                    services.AddSingleton<ConverterViewModel>();
                    services.AddSingleton<SettingsViewModel>();
                    services.AddSingleton<HistoryViewModel>();
                    services.AddSingleton<DetailsViewModel>();
                }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            MainView mainView = AppHost!.Services.GetRequiredService<MainView>();
            mainView.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
