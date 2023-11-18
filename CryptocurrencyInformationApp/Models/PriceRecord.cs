using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
