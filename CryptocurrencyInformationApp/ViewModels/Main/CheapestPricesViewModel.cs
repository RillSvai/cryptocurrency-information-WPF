using CryptocurrencyInformationApp.Data;
using CryptocurrencyInformationApp.Models;
using MaterialDesignThemes.Wpf.Converters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class CheapestPricesViewModel : ViewModelBase
    {
        IHttpClientFactory _httpClientFactory;
        IServiceProvider _serviceProvider;
        const int c_count = 10;
        DataGridAssetExchangerPrices[] _assetExchangerPrices;
        public string AssetId { get; set; }
        public DataGridAssetExchangerPrices[] AssetExchangerPrices 
        {
            get => _assetExchangerPrices.ToArray();
        }
        public ICommand BackCommand { get; }
        public ICommand BackToHomeCommand { get; }
        public CheapestPricesViewModel(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
            _assetExchangerPrices = new DataGridAssetExchangerPrices[c_count];
            AssetId = "bitcoin";
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
            BackToHomeCommand = new ViewModelCommand(ExecuteBackToHomeCommand);
        }

        private void ExecuteBackToHomeCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.ShowHomeViewCommand.Execute(null);
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            DetailsViewModel detailsViewModel = _serviceProvider.GetRequiredService<DetailsViewModel>();
            mainViewModel.Caption = $"home/details/{AssetId}";
            mainViewModel.CurrentChild = detailsViewModel;
        }

        public async Task LoadCheapestExchangers(string assetId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("coin-cap-api");
            HttpResponseMessage response = await httpClient.GetAsync($"assets/{assetId}/markets");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Something went wrong during getting prices in different markets for asset with id: {assetId} ");
            }
            string data = await response.Content.ReadAsStringAsync();
            CoinCapResponse<List<AssetExchangerPrices>>? coinCapResponse = new();
            coinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<List<AssetExchangerPrices>>>(data);
            _assetExchangerPrices = coinCapResponse!.Data
                !.Select((aep, i) => new
                {
                    PriceUsd = aep.PriceUsd,
                    Exchanger = Storage.Exchangers
                    .FirstOrDefault(e => e.Id.ToLower().Replace(" ", "") == aep.ExchangerId.ToLower().Replace(" ", ""))
                })
                .Select(a => new DataGridAssetExchangerPrices
                {
                    ExchangerName = a?.Exchanger?.Name,
                    ExchangerUrl = a?.Exchanger?.Url,
                    PriceUsd = a!.PriceUsd,
                })
                .Where(aep => aep.ExchangerName is not null && aep.ExchangerUrl is not null)
                .OrderBy(aep => aep.PriceUsd)
                .Take(c_count)
                .Select((aep,i) => new DataGridAssetExchangerPrices 
                {
                    ExchangerName = aep.ExchangerName,
                    ExchangerUrl = aep.ExchangerUrl,
                    PriceUsd = aep.PriceUsd,
                    Rank = i+1
                })
                .ToArray();
        }
    }
}
