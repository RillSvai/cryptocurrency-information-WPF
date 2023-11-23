using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptocurrencyInformationApp.Views
{
    public partial class CheapestPricesView : UserControl
    {
        public CheapestPricesView()
        {
            InitializeComponent();
        }
        private void exchangerUrl_Click(object sender, RoutedEventArgs e) 
        {
            if (sender is not TextBlock textBlock)
            {
                throw new ArgumentException("Expected another type of argument!");
            }
            string? url = (textBlock.DataContext as DataGridAssetExchangerPrices)!.ExchangerUrl;
            url?.OpenUrl();
        }
    }
}
