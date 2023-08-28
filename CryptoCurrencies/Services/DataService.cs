using CryptoCurrencies.Helpers;
using CryptoCurrencies.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoCurrencies.Services
{
    public class DataService : IDataService
    {
        List<Currency> Currencies = new List<Currency>();
        List<HistoryItem> History = new List<HistoryItem>();


        HttpClient httpClient = new HttpClient();

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync(uint limit, uint offset)
        {
            Currencies.Clear();
            string query = $"https://api.coincap.io/v2/assets?offset={offset}&limit={limit}";

            string json = await httpClient.GetStringAsync(query);

            var tmp = JObject.Parse(json).Children().First().First();

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            foreach (JToken item in tmp)
            {
                Currency cur = new Currency();
                try
                {
                    string jtokenString = item.ToString();
                    cur = JsonConvert.DeserializeObject<Currency>(jtokenString, jsonSettings);
                    cur.IconUrl = $"https://assets.coincap.io/assets/icons/{cur.Symbol.ToLower()}@2x.png";
                    cur.FormattedPrice = cur.PriceUsd.ToString("c2", new CultureInfo("en-US"));
                    cur.FormattedMarketCap = NumberFormatter.DecimalToShortPriceWithPostfix(cur.MarketCapUsd);
                    cur.FormattedVwap24Hr = cur.Vwap24Hr.ToString("c2", new CultureInfo("en-US"));
                    cur.FormattedSupply = NumberFormatter.DecimalToShortPriceWithPostfix(cur.Supply);
                    cur.FormattedVolume = NumberFormatter.DecimalToShortPriceWithPostfix(cur.VolumeUsd24Hr);
                    cur.FormattedChange = String.Format("{0:0.00}%", cur.ChangePercent24Hr);
                }
                catch (Exception)
                {
                    throw;
                }

                Currencies.Add(cur);
            }

            return Currencies;
        }
        public async Task<IEnumerable<Currency>> GetCurrenciesAsync(string search)
        {
            Currencies.Clear();

            if (!string.IsNullOrEmpty(search))
            {
                string query = $"https://api.coincap.io/v2/assets?search={search}";
                string json = await httpClient.GetStringAsync(query);
                var tmp = JObject.Parse(json).Children().First().First();
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                foreach (JToken item in tmp)
                {

                    Currency cur = new Currency();
                    try
                    {
                        string jtokenString = item.ToString();
                        cur = JsonConvert.DeserializeObject<Currency>(jtokenString, jsonSettings);
                        cur.IconUrl = $"https://assets.coincap.io/assets/icons/{cur.Symbol.ToLower()}@2x.png";
                        cur.FormattedPrice = cur.PriceUsd.ToString("c2", new CultureInfo("en-US"));
                        cur.FormattedMarketCap = NumberFormatter.DecimalToShortPriceWithPostfix(cur.MarketCapUsd);
                        cur.FormattedVwap24Hr = cur.Vwap24Hr.ToString("c2", new CultureInfo("en-US"));
                        cur.FormattedSupply = NumberFormatter.DecimalToShortPriceWithPostfix(cur.Supply);
                        cur.FormattedVolume = NumberFormatter.DecimalToShortPriceWithPostfix(cur.VolumeUsd24Hr);
                        cur.FormattedChange = String.Format("{0:0.00}%", cur.ChangePercent24Hr);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Currencies.Add(cur);
                }
            }
            return Currencies;
        }
        public async Task<IEnumerable<HistoryItem>> GetCurrencyHistoryByIdAsync(string id)
        {
            History.Clear();

            string query = $"http://api.coincap.io/v2/assets/{id}/history?interval=m1";
            string json = await httpClient.GetStringAsync(query);
            var tmp = JObject.Parse(json).Children().First().First();

            foreach (JToken item in tmp)
            {
                try
                {
                    HistoryItem history = new HistoryItem();
                    history = JsonConvert.DeserializeObject<HistoryItem>(item.ToString());
                    History.Add(history);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return History;
        }

    }
}