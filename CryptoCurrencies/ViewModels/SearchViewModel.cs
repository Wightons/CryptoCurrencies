using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrencies.ViewModels
{
    public class SearchViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        public ObservableCollection<Currency> Currencies { get; set; } = new ObservableCollection<Currency>();

        private string searchQuery;

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;
                OnPropertyChanged();
            }
        }

        private uint currentOffset = 0;
        private uint offsetStep = 20;

        public ICommand LoadMoreCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ICommand OpenInfoCommand { get; set; }

        private readonly MainViewModel _mainViewModel;
        

        public SearchViewModel(IDataService dataService, MainViewModel mainViewModel)
        {
            _dataService = dataService;
            _mainViewModel = mainViewModel;
            
            Task.Run(async () =>
            {
                var tmp = await _dataService.GetCurrenciesAsync(20, currentOffset);
                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var item in tmp)
                    {
                        Currencies.Add(item);
                    }
                });
            });

            InitCommands();


        }

        private void InitCommands()
        {

            LoadMoreCommand = new RelayCommand(async param =>
            {
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {

                    currentOffset += offsetStep;
                    var toAdd = await _dataService.GetCurrenciesAsync(20, currentOffset);
                    foreach (var item in toAdd)
                    {
                        Currencies.Add(item);
                    }
                }
            });

            RefreshCommand = new RelayCommand(async param =>
            {
                SearchQuery = "";
                Currencies.Clear();
                currentOffset = 0;
                var toAdd = await _dataService.GetCurrenciesAsync(20, currentOffset);
                foreach (var item in toAdd)
                {
                    Currencies.Add(item);
                }
            });

            SearchCommand = new RelayCommand(async param =>
            {
                Currencies.Clear();
                var res = await _dataService.GetCurrenciesAsync(SearchQuery);

                foreach (var item in res)
                {
                    Currencies.Add(item);
                }

            });

            OpenInfoCommand = new RelayCommand(param =>
            {
                if (param != null)
                {
                    _mainViewModel.CurrentViewModel = new InfoViewModel(_dataService, (Currency)param);
                }
                
            });
        }

    }
}
