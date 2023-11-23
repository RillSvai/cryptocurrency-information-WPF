using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using System.Collections.Generic;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class HistoryViewModel : ViewModelBase
    {
        private readonly IXmlHelper _xmlHelper;
        private List<HistoryRecord> _records;
        private const string c_path = "Data/History.xml";
        public List<HistoryRecord> Records
        {
            get => _records;
            set 
            {
                _records = value;
                OnPropertyChanged(nameof(Records));
            }
        }
        public ICommand ClearHistory { get; }
        public HistoryViewModel(IXmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
            ClearHistory = new ViewModelCommand(ExecuteClearHistory);
        }

        private void ExecuteClearHistory(object obj)
        {
            _xmlHelper.WriteEmptyCollection<HistoryRecord>(c_path);
            Records = new List<HistoryRecord>();
        }

        public void LoadHistory() 
        {
            _records = _xmlHelper.GetElements<HistoryRecord>(c_path);
        }
    }
}
