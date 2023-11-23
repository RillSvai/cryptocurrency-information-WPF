using Newtonsoft.Json;

namespace CryptocurrencyInformationApp.Models
{
    public class CoinCapResponse<T>
    {
        [JsonProperty("data")]
        public T? Data {  get; set; }
    }
}
