using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.Models
{
    public class Asset : ICloneable
    {
        [JsonProperty("id")]
        public required string Id { get; set; }

        [JsonProperty("rank")]
        public required int Rank { get; set; }

        [JsonProperty("symbol")]
        public required string Symbol { get; set; }

        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("supply")]
        public required decimal Supply { get; set; }

        [JsonProperty("maxSupply")]
        public decimal? MaxSupply { get; set; }

        [JsonProperty("marketCapUsd")]
        public required decimal MarketCapUsd { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        public required decimal VolumeUsd24Hr { get; set; }

        [JsonProperty("priceUsd")]
        public required decimal PriceUsd { get; set; }

        [JsonProperty("explorer")]
        public string? Explorer { get; set; }

		public object Clone()
		{
            return new Asset()
            {
                Id = Id,
                Rank = Rank,
                Symbol = Symbol,
                Name = Name,
                Supply = Supply,
                MaxSupply = MaxSupply,
                MarketCapUsd = MarketCapUsd,
                VolumeUsd24Hr = VolumeUsd24Hr,
                PriceUsd = PriceUsd,
                Explorer = Explorer
            };
		}
	}
}
