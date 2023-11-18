using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.Models
{
    public class DataGridAssetExchangerPrices
    {
        public int Rank { get; set; }
        public string ExchangerName { get; set; }
        public decimal PriceUsd { get; set; }
        public string ExchangerUrl { get; set; }
    }
}
