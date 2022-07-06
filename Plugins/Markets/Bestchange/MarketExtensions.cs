using MarketPlugin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bestchange
{
    internal static class Extensions
    {
        internal static BestChangeRate GetRate(this List<BestChangeRate> data, Pair pair)
        {
            BestChangeRate rate = null;
            if (pair.From == Currency.RUB)
            {
                if (pair.To == Currency.BTC)
                    rate = data.FirstOrDefault(x => x.From == BCCurrencies.Sberbank && x.To == BCCurrencies.Bitcoin_BTC);
            }
            if (pair.From == Currency.USD)
            {
                if (pair.To == Currency.BTC)
                    rate = data.FirstOrDefault(x => x.From == BCCurrencies.VisaMasterCardUSD && x.To == BCCurrencies.Bitcoin_BTC);
            }
            if (pair.From == Currency.EUR)
            {
                if (pair.To == Currency.BTC)
                    rate = data.FirstOrDefault(x => x.From == BCCurrencies.VisaMasterCardEUR && x.To == BCCurrencies.Bitcoin_BTC);
            }

            return rate;
        }
    }
}
