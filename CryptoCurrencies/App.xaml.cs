using Autofac;
using CryptoCurrencies.Services;
using CryptoCurrencies.ViewModels;
using System.Windows;

namespace CryptoCurrencies
{
    public partial class App : Application
    {
        private IContainer _container;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            var main = new MainWindow();
            main.Show();
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            
            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<SearchViewModel>().SingleInstance();
            builder.RegisterType<InfoViewModel>();

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

    }
}
