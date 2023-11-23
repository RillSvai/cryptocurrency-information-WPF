using CryptocurrencyInformationApp.Data;
using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class ConverterViewModel : ViewModelBase
    {
        private string _inputNumber;
        private string _outputNumber;
        private ConverterComboBoxItem[] _items;
        private decimal _inputPrice;
        private decimal _outputPrice;
        private readonly INumberValidator _numberValidator;
        public decimal InputPrice 
        {
            get => _inputPrice;
            set 
            {
                _inputPrice = value;
                OnPropertyChanged(nameof(InputPrice));
            }
        }
        public decimal OutputPrice 
        {
            get => _outputPrice;
            set 
            {
                _outputPrice = value;
                OnPropertyChanged(nameof(OutputPrice));
            }
        }
        public string InputNumber 
        {
            get => _inputNumber;
            set 
            {
                if (IsInputChangeAllowed(value)) 
                {
                    _inputNumber = value;
                    OnPropertyChanged(nameof(InputNumber));
                }
            }
        }
        public string OutputNumber 
        {
            get => _outputNumber;
            set 
            {
                _outputNumber = value;
                OnPropertyChanged(nameof(OutputNumber));
            }
        }
        public ConverterComboBoxItem[] Items 
        {
            get => _items
                .Select(item => new ConverterComboBoxItem { Value = item.Value, DisplayValue = item.DisplayValue})
                .ToArray();
        }
        public ICommand ConvertCommand { get; }
        public ConverterViewModel(INumberValidator numberValidator)
        {
            _items = Storage.Assets
                .OrderBy(a => a.Name)
                .Select(a => new ConverterComboBoxItem {DisplayValue = a.Name, Value = a.PriceUsd})
                .ToArray();
            _numberValidator = numberValidator;
            ConvertCommand = new ViewModelCommand(ExecuteConvertCommand, CanExecuteConvertCommand);
            InputPrice = _items[9].Value;
            OutputPrice = _items[30].Value;
        }

        private bool CanExecuteConvertCommand(object obj)
        {
            if (string.IsNullOrEmpty(InputNumber?.Trim()) || decimal.Parse(InputNumber) == 0) 
            {
                return false;
            }
            return true;
        }

        private void ExecuteConvertCommand(object obj)
        {
            decimal ratio = InputPrice / OutputPrice;
            OutputNumber = Math.Round((decimal.Parse(InputNumber) * ratio),8).ToString();
        }

        private bool IsInputChangeAllowed(string value) 
        {
            if (string.IsNullOrEmpty(value)) 
            {
                return true;
            }
            bool isDecimal = decimal.TryParse(value, out decimal num);
            if (!isDecimal || !_numberValidator.IsInRange<decimal>(num, 0, 100000000000) 
                || Regex.IsMatch(value, @"^0[0-9]") || value.Length > 20 || value.Contains(" ")) 
            {
                return false;
            }
            return true;
        }
    }
}
