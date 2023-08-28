using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.Helpers
{
    public class NumberFormatter
    {
        public static string DecimalToShortPriceWithPostfix(decimal number)
        {
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
            if (Math.Abs(number) >= 1_000_000_000)
            {
                return (number / 1_000_000_000).ToString("C2", cultureInfo) + 'b';
            }
            else if (Math.Abs(number) >= 1_000_000)
            {
                return (number / 1_000_000).ToString("C2", cultureInfo) + 'm';
            }
            else if (Math.Abs(number) >= 1_000)
            {
                return (number / 1_000).ToString("C2", cultureInfo) + 'k';
            }
            else
            {
                return number.ToString("C2", cultureInfo);
            }
        }
    }
}
