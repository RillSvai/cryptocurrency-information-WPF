using CryptocurrencyInformationApp.Data;
using CryptocurrencyInformationApp.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.Utility
{
    public static class AppExtensions
    {
        public static async Task SeedData(this IHost host) 
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri("https://api.coincap.io/v2/") })
            {
                HttpResponseMessage assetsResponse = await client.GetAsync("assets");
                HttpResponseMessage exchangersResponse = await client.GetAsync("exchanges");
                if (!assetsResponse.IsSuccessStatusCode || !exchangersResponse.IsSuccessStatusCode ) 
                {
                    throw new HttpRequestException("Unsuccessfull request(s) from api.coincap.io!");
                }
                string? assets = await assetsResponse.Content.ReadAsStringAsync();
                string? exchangers = await exchangersResponse.Content.ReadAsStringAsync();
                CoinCapResponse<List<Asset>> assetsCoinCapResponse = new();
                CoinCapResponse<List<Exchanger>> exchangersCoinCapResponse = new();
                assetsCoinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<List<Asset>>>(assets)!;
                exchangersCoinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<List<Exchanger>>>(exchangers)!;
                Storage.Assets = assetsCoinCapResponse.Data!;
                Storage.Exchangers = exchangersCoinCapResponse.Data!;
            }
        }
    }
}
