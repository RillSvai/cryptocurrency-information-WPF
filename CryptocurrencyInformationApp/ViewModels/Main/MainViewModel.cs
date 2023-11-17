using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChild;
        private string _caption;
        private IconChar _icon;
        private readonly HomeViewModel _homeViewModel;
        private readonly ConverterViewModel _converterViewModel;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly HistoryViewModel _historyViewModel;

        public string Caption 
        {
            get => _caption;
            set 
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get => _icon;
            set 
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public ViewModelBase CurrentChild 
        {
            get => _currentChild;
            set 
            {
                _currentChild = value;
                OnPropertyChanged(nameof(CurrentChild));
            }
        }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowStatisticViewCommand {  get; }
        public ICommand ShowConverterViewCommand {  get; }
        public ICommand ShowHistoryViewCommand {  get; }
        public ICommand ShowSettingsViewCommand {  get; }
        public MainViewModel
            (HomeViewModel homeViewModel,ConverterViewModel converterViewModel,
            SettingsViewModel settingsViewModel, HistoryViewModel historyViewModel)
        {
            _homeViewModel = homeViewModel;
            _converterViewModel = converterViewModel;
            _settingsViewModel = settingsViewModel;
            _historyViewModel = historyViewModel;
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);
            ShowConverterViewCommand = new ViewModelCommand(ExecuteShowConverterViewCommand);
            ShowHistoryViewCommand = new ViewModelCommand(ExecuteShowHistoryViewCommand);
            ExecuteShowHomeViewCommand(null);
        }

        private void ExecuteShowHistoryViewCommand(object obj)
        {
            Icon = IconChar.Book;
            Caption = "History";
            CurrentChild = _historyViewModel;
        }

        private void ExecuteShowConverterViewCommand(object obj)
        {
            Icon = IconChar.ArrowRightArrowLeft;
            Caption = "Converter";
            CurrentChild = _converterViewModel;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            Icon = IconChar.Gears;
            Caption = "Settings";
            CurrentChild = _settingsViewModel;
        }

        private void ExecuteShowHomeViewCommand(object? obj)
        {
            Icon = IconChar.Home;
            Caption= "Home";
            CurrentChild = _homeViewModel;
        }
    }
}
