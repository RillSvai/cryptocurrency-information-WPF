using AutoMapper;
using CryptocurrencyInformationApp.Data;
using CryptocurrencyInformationApp.Utility;
using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using CryptocurrencyInformationApp.Utility.Services.Implementations;
using CryptocurrencyInformationApp.ViewModels.Main;
using CryptocurrencyInformationApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                    //Views and view models
                    services.AddSingleton<MainView>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<HomeViewModel>();
                    services.AddSingleton<ConverterViewModel>();
                    services.AddSingleton<SettingsViewModel>();
                    services.AddSingleton<HistoryViewModel>();
                    services.AddSingleton<DetailsViewModel>();
                    services.AddSingleton<PriceHistoryViewModel>();
                    services.AddSingleton<CheapestPricesViewModel>();
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    //API
                    services.AddHttpClient("coin-cap-api", httpClient => 
                    {
                        httpClient.BaseAddress = new Uri("https://api.coincap.io/v2/");
                    });
                    //Custom services
                    services.AddTransient<INumberValidator, NumberValidator>();
                    services.AddTransient<IXmlHelper, XmlHelper>();
                }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            await AppHost.SeedData();
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
