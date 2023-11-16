using CryptocurrencyInformationApp.Models;
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
            try
            {
                Process.Start(url!);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url!.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url!);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url!);
                }
                else
                {
                    throw new Exception("Something went wrong with navigation!");
                }
            }
        }
    }
}
