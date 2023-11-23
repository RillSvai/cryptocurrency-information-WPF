using Newtonsoft.Json;
using System;

namespace CryptocurrencyInformationApp.Models
{
    public class Exchanger : ICloneable
    {
        [JsonProperty("exchangeId")]
        public required string Id { get; set; }

        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("rank")]
        public required int Rank { get; set; }

        [JsonProperty("volumeUsd")]
        public required decimal? VolumeUsd { get; set; }

        [JsonProperty("exchangeUrl")]
        public required string Url { get; set; }

		public object Clone()
		{
            return new Exchanger()
            {
                Id = Id,
                Name = Name,
                Rank = Rank,
                VolumeUsd = VolumeUsd,
                Url = Url
            };
		}
	}
}
