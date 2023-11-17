using CryptocurrencyInformationApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class DetailsViewModel : ViewModelBase
    {
        private Asset? _asset;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        public Asset Asset 
        {
            get => _asset;
            set 
            {
                _asset = value;
                OnPropertyChanged(nameof(Asset));
            }
        }
        public DetailsViewModel(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
        }
        public async Task LoadSelectedAsset(string assetId) 
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
