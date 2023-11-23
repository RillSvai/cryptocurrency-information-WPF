
namespace CryptocurrencyInformationApp.Models
{
    public class DataGridAsset
    {
        public required string Id { get; set; }
        public required int Rank { get; set; }
        public required string Name { get; set; }
        public required decimal PriceUsd { get; set; }
        public string? Explorer { get; set; }
    }
}
