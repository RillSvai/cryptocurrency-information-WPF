using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility;
using CryptocurrencyInformationApp.ViewModels.Main;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CryptocurrencyInformationApp.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }
        private void explorer_Click(object sender, RoutedEventArgs e) 
        {
            if (sender is not TextBlock textBlock) 
            {
                throw new ArgumentException("Expected another type of argument!");
            }
            string? url = (textBlock.DataContext as DataGridAsset)!.Explorer;
            url?.OpenUrl();
        }
    }
}
