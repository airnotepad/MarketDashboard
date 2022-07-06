using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlugin.Base
{
    public class Pair
    {
        public Pair(Currency From, Currency To)
        {
            this.From = From;
            this.To = To;
        }

        public Currency From { get; }
        public Currency To { get; }
    }
}
