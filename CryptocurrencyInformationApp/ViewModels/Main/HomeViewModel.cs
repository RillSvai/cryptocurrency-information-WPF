using CryptocurrencyInformationApp.Data;
using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class HomeViewModel : ViewModelBase
    {
        private string _rowsLimit;
        private int _rowsCount;
        private string _searchFilte;
        private List<string> _searchOptions;
        private List<Asset> _assets;
        private IEnumerable<Asset> _assetsRepresantion;
        private readonly INumberValidator _numberValidator;
        public string RowsLimit 
        {
            get => _rowsLimit;
            set 
            {
                if (IsRowsLimitChangeAllowed(value)) 
                { 
                    _rowsLimit = value;
                    OnPropertyChanged(nameof(RowsLimit));
                    OnRowsLimitChanged();
                }
                
            }
        }
        public int RowsCount 
        {
            get => _rowsCount;
            set 
            {
                _rowsCount = value;
                OnPropertyChanged(nameof(RowsCount));
            }
        }
        public IEnumerable<Asset> AssetsRepresantion
        {
            get => _assetsRepresantion;
            set 
            {
                _assetsRepresantion = value;
                OnPropertyChanged(nameof(AssetsRepresantion));
            }
        }
        public ICommand ShowDetailsViewCommand { get; }
        public HomeViewModel(INumberValidator numberValidator)
        {
            _numberValidator = numberValidator;
            _assets = Storage.Assets;
            AssetsRepresantion = Storage.Assets;
            RowsLimit = _assets.Count.ToString();
            RowsCount = _assets.Count;
            ShowDetailsViewCommand = new ViewModelCommand(ExecuteShowDetailsViewCommand);
        }

        private void ExecuteShowDetailsViewCommand(object obj)
        {
            if (obj is Asset asset) 
            {

            }
            MessageBox.Show(obj.ToString());
        }

        private bool IsRowsLimitChangeAllowed(string value) 
        {
            if (string.IsNullOrWhiteSpace(value)) 
            {
                return true;
            }   
            bool isInt = int.TryParse(value, out int num);
            if (!isInt || !_numberValidator.IsInRange(num, 1, _assets.Count))
            {
                return false;
            }
            return true;
        }
        private void OnRowsLimitChanged() 
        {
            int num = string.IsNullOrEmpty(RowsLimit?.Trim()) ? 0 : int.Parse(RowsLimit);
            AssetsRepresantion = _assets.Take(num);
            RowsCount = AssetsRepresantion.Count();
        }
    }
}
