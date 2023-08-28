using CryptoCurrencies.Core;
using CryptoCurrencies.Helpers;
using CryptoCurrencies.Models;
using CryptoCurrencies.Services;
using CryptoCurrencies.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CryptoCurrencies.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        private object currentViewModel;

        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged(); }
        }
        public ICommand SwitchPagesCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }
        

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            
            CurrentViewModel = new TopChartViewModel(_dataService);
            InitCommands();

        }

        private void InitCommands()
        {
            SwitchPagesCommand = new RelayCommand(param =>
            {

                switch (param)
                {
                    case "1":

                        CurrentViewModel = new TopChartViewModel(_dataService);
                        break;
                    case "2":
                        CurrentViewModel = new SearchViewModel(_dataService, this);
                        break;
                }
            });

            ChangeThemeCommand = new RelayCommand(param =>
            {
                switch (param)
                {
                    case "Light":
                        AppTheme.ChangeTheme(new System.Uri("Themes/Light.xaml", System.UriKind.Relative));
                        break;
                    case "Dark":
                        AppTheme.ChangeTheme(new System.Uri("Themes/Dark.xaml", System.UriKind.Relative));
                        break;
                }
            });
        }

    }
}
