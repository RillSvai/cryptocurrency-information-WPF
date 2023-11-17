using Newtonsoft.Json;
using System.Collections.Generic;

namespace CryptocurrencyInformationApp.Models
{
    public class CoinCapResponse<T>
    {
        [JsonProperty("data")]
        public T? Data {  get; set; }
    }
}
