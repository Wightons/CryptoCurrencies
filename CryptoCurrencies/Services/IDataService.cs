using CryptoCurrencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Models.Currency>> GetCurrenciesAsync(uint limit, uint offset);
        Task<IEnumerable<Currency>> GetCurrenciesAsync(string search);
        Task<IEnumerable<HistoryItem>> GetCurrencyHistoryByIdAsync(string id);
    }
}
