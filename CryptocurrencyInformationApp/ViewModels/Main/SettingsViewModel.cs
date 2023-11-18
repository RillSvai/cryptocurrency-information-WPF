using CryptocurrencyInformationApp.Models;
using System;
using System.Windows;
namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class SettingsViewModel : ViewModelBase
    {
        private ComboBoxItem[] _themeOptions;
        private ComboBoxItem[] _fontOptions;
        private string _selectedTheme;
        private string _selectedFont;
        public ComboBoxItem[] ThemeOptions => _themeOptions;
        public ComboBoxItem[] FontOptions => _fontOptions;
        public string SelectedTheme 
        {
            get => _selectedTheme;
            set 
            {
                _selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));
                OnThemeChanged(value);
            }
        }
        public string SelectedFont 
        {
            get => _selectedFont;
            set 
            {
                _selectedFont = value;
                OnPropertyChanged(nameof(SelectedFont));
                OnFontChanged(value);
            }
        }
        public SettingsViewModel()
        {
            _themeOptions = new ComboBoxItem[]
            {
                new ComboBoxItem() {Value = "/Styles/BlueTheme.xaml", DisplayValue = "Blue theme"},
                new ComboBoxItem() {Value = "/Styles/RedTheme.xaml", DisplayValue = "Red theme"},
            };
            _fontOptions = new ComboBoxItem[]
            {
                new ComboBoxItem() {Value = "/Styles/FunFonts.xaml", DisplayValue = "Fun fonts"},
                new ComboBoxItem() {Value = "/Styles/CoolFonts.xaml", DisplayValue = "Cool fonts"},
                new ComboBoxItem() {Value = "/Styles/GothicFonts.xaml", DisplayValue = "Gothic fonts"},
            };
            SelectedTheme = _themeOptions[1].Value;
            SelectedFont = _fontOptions[1].Value;
        }
        private void OnThemeChanged(string path) 
        {
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri(path,System.UriKind.Relative)};
        }
        private void OnFontChanged(string path) 
        {
            Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary() { Source = new Uri(path, System.UriKind.Relative) };
        }
    }
}
