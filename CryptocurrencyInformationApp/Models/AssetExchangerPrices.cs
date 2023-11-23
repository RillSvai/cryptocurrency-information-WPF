using Newtonsoft.Json;

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
