using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class CheapestPricesViewModel : ViewModelBase
    {
        IHttpClientFactory _httpClientFactory;
        IServiceProvider _serviceProvider;
        public CheapestPricesViewModel(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
        }
        public async Task LoadCheapestExchangers(string assetId) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("coin-cap-api");
            HttpResponseMessage response = await httpClient.GetAsync($"assets/{assetId}/markets");
            if (!response.IsSuccessStatusCode) 
            {
                throw new HttpRequestException($"Something went wrong during getting prices in different markets for asset with id: {assetId} ");            
            }

        }
    }
}
