using CryptoCurrencies.Models;
using CryptoCurrencies.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CryptoCurrencies.ViewModels
{
    public class TopChartViewModel
    {

        private readonly IDataService _dataService;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public TopChartViewModel(IDataService dataService)
        {
            _dataService = dataService;

            SeriesCollection = new SeriesCollection();

            Task.Run(async () =>
            {
                var res = await _dataService.GetCurrenciesAsync(10, 0);

                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var item in res)
                    {
                        var cs = new RowSeries();
                        cs.Title = item.Name;
                        cs.Values = new ChartValues<decimal>(new List<decimal> { item.PriceUsd });
                        SeriesCollection.Add(cs);
                    }
                });
            });

        }
    }
}
