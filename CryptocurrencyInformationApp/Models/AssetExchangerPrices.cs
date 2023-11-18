using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.Models
{
    public class AssetExchangerPrices
    {
        [JsonProperty("exchangeId")]
        public string ExchangerId { get; set; }

        [JsonProperty("priceUsd")]
        public decimal PriceUsd { get; set; }
    }
}
