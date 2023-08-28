using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace CryptoCurrencies.ViewModels
{
    public class InfoViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

		private Currency currency;

		public Currency Currency
        {
			get { return currency; }
			set { currency = value; OnPropertyChanged(); }
		}
        public SeriesCollection HistoryItems { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public InfoViewModel(IDataService dataService, Currency currency)
        {
            _dataService = dataService;
            HistoryItems = new SeriesCollection();

            Task.Run(async () =>
            {
                var res = await _dataService.GetCurrencyHistoryByIdAsync(currency.Id);
                try
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {

                        var chartValues = new ChartValues<ObservablePoint>();
                        foreach (var item in res)
                        {
                            var tmptime = (double)item.Time;
                            var tmpPrice = (double)item.PriceUsd;
                            chartValues.Add(new ObservablePoint(tmpPrice, tmptime));
                        }

                        var lineSeries = new LineSeries
                        {
                            Values = new ChartValues<ObservablePoint>(chartValues.ToList())
                        };

                        HistoryItems.Add(lineSeries);
                        YFormatter = value => value.ToString("N0");
                    });
                    
                }
                catch (Exception)
                {

                    throw;
                }
            });

            Currency = currency ?? Currency;

        }

    }
}
