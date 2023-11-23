using Newtonsoft.Json;
using System;

namespace CryptocurrencyInformationApp.Models
{
    public class PriceRecord
    {
        [JsonProperty("priceUsd")]
        public decimal Price { get; set; }
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }
}
