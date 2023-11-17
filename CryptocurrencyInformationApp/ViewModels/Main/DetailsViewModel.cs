using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class DetailsViewModel : ViewModelBase
    {
        private string _assetId;
        private readonly IServiceProvider _serviceProvider;

        public string AssetId 
        {
            get => _assetId;
            set => _assetId = value;
        }
        public DetailsViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

    }
}
