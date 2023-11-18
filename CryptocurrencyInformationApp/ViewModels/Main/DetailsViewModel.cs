using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class DetailsViewModel : ViewModelBase
    {
        private Asset? _asset;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CheapestPricesViewModel _cheapestPricesViewModel;
        private readonly PriceHistoryViewModel _priceHistoryViewModel;
        public Asset Asset 
        {
            get => _asset;
            set 
            {
                _asset = value;
                OnPropertyChanged(nameof(Asset));
            }
        }
        public ICommand NavigateToUrlCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ShowPriceHistoryViewCommand { get; }
        public ICommand ShowCheapestPricesViewCommand { get; }
        public DetailsViewModel(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory,
            CheapestPricesViewModel cheapestPricesViewModel, PriceHistoryViewModel priceHistoryViewModel)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
            _priceHistoryViewModel = priceHistoryViewModel;
            _cheapestPricesViewModel = cheapestPricesViewModel;
            NavigateToUrlCommand = new ViewModelCommand(ExecuteNavigateToUrlCommand);
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
            ShowCheapestPricesViewCommand = new ViewModelCommand(ExecuteShowCheapestPricesViewCommand);
            ShowPriceHistoryViewCommand = new ViewModelCommand(ExecuteShowPriceHistoryViewCommand);
        }

        private async void ExecuteShowPriceHistoryViewCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.Caption = $"home/details/price-history/{Asset.Id}";
            _priceHistoryViewModel.AssetId = Asset.Id;
            await _priceHistoryViewModel.LoadPriceHistory(Asset.Id);
            mainViewModel.CurrentChild = _priceHistoryViewModel;
        }

        private async void ExecuteShowCheapestPricesViewCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            _cheapestPricesViewModel.AssetId = Asset.Id;
            mainViewModel.Caption = $"home/details/cheapest-prices/{Asset.Id}";
            await _cheapestPricesViewModel.LoadCheapestExchangers(Asset.Id);
            mainViewModel.CurrentChild = _cheapestPricesViewModel;
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.ShowHomeViewCommand.Execute(null);
        }

        private void ExecuteNavigateToUrlCommand(object obj)
        {
            if (obj is not string url) 
            {
                throw new ArgumentException("Expected argument of type: string");
            }
            url.OpenUrl();
        }

        public async Task LoadSelectedAssetAsync(string assetId) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("coin-cap-api");
            HttpResponseMessage response = await httpClient.GetAsync($"assets/{assetId}");
            if (!response.IsSuccessStatusCode) 
            {
                throw new HttpRequestException($"Something went wrong during getting asset with id: {assetId}");
            }
            string data = await response.Content.ReadAsStringAsync();
            CoinCapResponse<Asset>? coinCapResponse = new CoinCapResponse<Asset>();
            coinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<Asset>>(data);
            Asset = coinCapResponse!.Data!;
        }
        

    }
}
