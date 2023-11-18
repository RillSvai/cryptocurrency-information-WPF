using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptocurrencyInformationApp.Views
{
    /// <summary>
    /// Interaction logic for CheapestPricesView.xaml
    /// </summary>
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
