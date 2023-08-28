using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.Models
{
    public class HistoryItem
    {
        public decimal PriceUsd { get; set; }
        public long Time { get; set; }
        public DateTime Date { get; set; }
    }
}
