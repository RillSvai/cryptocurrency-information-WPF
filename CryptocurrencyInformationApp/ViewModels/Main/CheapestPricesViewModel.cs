using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class CheapestPricesViewModel : ViewModelBase
    {
        private string? _assetId;
        public string? AssetId { get; set; }
    }
}
