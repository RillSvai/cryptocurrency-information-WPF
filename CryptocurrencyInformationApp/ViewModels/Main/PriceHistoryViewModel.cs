using CryptocurrencyInformationApp.Models;
using CryptocurrencyInformationApp.Utility;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CryptocurrencyInformationApp.ViewModels.Main
{
    public class PriceHistoryViewModel : ViewModelBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;
        private List<PriceRecord> _priceRecordsD1;
        private List<PriceRecord> _priceRecordsH1;
        private string[] _priceRecordsD1Options;
        private string[] _priceRecordsH1Options;
        private int _selectedPriceRecordD1Option;
        private int _selectedPriceRecordH1Option;
        private LineSeries<DateTimePoint>[] _seriesD1;
        private LineSeries<DateTimePoint>[] _seriesH1;
        public string AssetId { get; set; }

        public string[] PriceRecordsD1Options 
        {
            get => _priceRecordsD1Options;
        }
        public string[] PriceRecordsH1Options 
        {
            get => _priceRecordsH1Options;
        }
        public int SelectedPriceRecordD1Option 
        {
            get => _selectedPriceRecordD1Option;
            set 
            {
                _selectedPriceRecordD1Option = value;
                OnPropertyChanged(nameof(SelectedPriceRecordD1Option));
                OnSelectedD1OptionChanged(_priceRecordsD1Options[value]);

            }
        }
        public int SelectedPriceRecordH1Option 
        {
            get => _selectedPriceRecordH1Option;
            set
            {
                _selectedPriceRecordH1Option = value;
                OnPropertyChanged(nameof(SelectedPriceRecordH1Option));
                OnSelectedH1OptionChanged(_priceRecordsH1Options[value]);
            }
        }
        public LineSeries<DateTimePoint>[] SeriesD1 
        {
            get => _seriesD1;
            set 
            {
                _seriesD1 = value;
                OnPropertyChanged(nameof(SeriesD1));
            }
        }

        public LineSeries<DateTimePoint>[] SeriesH1 
        {
            get => _seriesH1;
            set 
            {
                _seriesH1 = value;
                OnPropertyChanged(nameof(SeriesH1));
            }
        }
        public Axis[] DayXAxes { get; } =
        {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd.MM"))
            {
                Name = "Days (UTC +0)",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White),
            }
        };
        public Axis[] YAxes { get; } =
        {
            new Axis
            {
                Name = "Price",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint (SKColors.White),
            }
        };
        public Axis[] HourXAxes { get; } =
        {
            new DateTimeAxis(TimeSpan.FromHours(1), date => date.ToString("HH:00"))
            {
                Name = "Hours (UTC +0) ",
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White),
            }
        };
        public ICommand BackCommand { get; }
        public ICommand BackToHomeCommand { get; }
        public PriceHistoryViewModel(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            _httpClientFactory = httpClientFactory;
            _serviceProvider = serviceProvider;
            _priceRecordsD1 = new List<PriceRecord>();
            _priceRecordsH1 = new List<PriceRecord>();
            BackCommand = new ViewModelCommand(ExecuteBackCommand);
            BackToHomeCommand = new ViewModelCommand(ExecuteBackToHomeCommand);
            _priceRecordsD1Options = new string[] {"Last 7 days", "Last 14 days", "Last 21 days", "Last 30 days"};
            _priceRecordsH1Options = new string[] { "Last 6 hours", "Last 12 hours", "Last 18 hours", "Last 24 hours" };
            SelectedPriceRecordD1Option = 1;
            SelectedPriceRecordH1Option = 1;
        }

        private void ExecuteBackToHomeCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.ShowHomeViewCommand.Execute(null);
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            DetailsViewModel detailsViewModel = _serviceProvider.GetRequiredService<DetailsViewModel>();
            mainViewModel.Caption = $"home/details/{AssetId}";
            mainViewModel.CurrentChild = detailsViewModel;
        }


        public void OnSelectedD1OptionChanged(string value) 
        {
            int count = value.GetFirstNumber() ?? 0;
            SeriesD1 = new LineSeries<DateTimePoint>[]
            {
                new LineSeries<DateTimePoint>()
                {
                    Values = _priceRecordsD1
                    .Take(count)
                    .Select(pr => new DateTimePoint(pr.Date.UtcDateTime, (double)pr.Price)),
                    
                }
            };
        }
        public void OnSelectedH1OptionChanged(string value) 
        {
            int count = value.GetFirstNumber() ?? 0;
            SeriesH1 = new LineSeries<DateTimePoint>[]
            {
                new LineSeries<DateTimePoint>()
                {
                    Values = _priceRecordsH1
                .Take(count)
                .Select(pr => new DateTimePoint(pr.Date.UtcDateTime, (double)pr.Price))
                }
            };
            
        }
        public async Task LoadPriceHistory(string assetId) 
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("coin-cap-api");
            HttpResponseMessage hourHistoryResponse = await httpClient.GetAsync($"assets/{assetId}/history?interval=h1");
            HttpResponseMessage dayHistoryResponse = await httpClient.GetAsync($"assets/{assetId}/history?interval=d1");
            if (!dayHistoryResponse.IsSuccessStatusCode || !hourHistoryResponse.IsSuccessStatusCode) 
            {
                throw new HttpRequestException($"Something went wrong during getting price history for asset with id: {assetId}");
            }
            string dayHistoryData = await dayHistoryResponse.Content.ReadAsStringAsync();
            string hourHistoryData = await hourHistoryResponse.Content.ReadAsStringAsync();
            CoinCapResponse<List<PriceRecord>> coinCapResponse = new CoinCapResponse<List<PriceRecord>>();
            coinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<List<PriceRecord>>>(dayHistoryData)!;
            _priceRecordsD1 = coinCapResponse.Data!;
            _priceRecordsD1.Reverse();
            coinCapResponse = JsonConvert.DeserializeObject<CoinCapResponse<List<PriceRecord>>>(hourHistoryData)!;
            _priceRecordsH1 = coinCapResponse.Data!;
            _priceRecordsH1.Reverse();
            OnSelectedD1OptionChanged(_priceRecordsD1Options[_selectedPriceRecordD1Option]);
            OnSelectedH1OptionChanged(_priceRecordsH1Options[_selectedPriceRecordH1Option]);
        }
    }
}
