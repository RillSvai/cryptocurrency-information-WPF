using Newtonsoft.Json;
using System.Collections.Generic;

namespace CryptocurrencyInformationApp.Models
{
    public class CoinCapResponse<T>
    {
        [JsonProperty("data")]
        public List<T>? Data {  get; set; }
    }
}
