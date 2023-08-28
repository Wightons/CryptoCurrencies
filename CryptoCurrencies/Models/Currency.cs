using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.Models
{
    public class Currency
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal Supply { get; set; }
        public decimal MaxSupply { get; set; }
        public decimal MarketCapUsd { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal ChangePercent24Hr { get; set; }
        public decimal Vwap24Hr { get; set; }


        public string IconUrl { get; set; }
        public string FormattedPrice { get; set; }
        public string FormattedMarketCap { get; set; }
        public string FormattedVwap24Hr { get; set; }
        public string FormattedSupply { get; set; }
        public string FormattedVolume { get; set; }
        public string FormattedChange { get; set; }

    }
}
