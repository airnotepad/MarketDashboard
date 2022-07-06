namespace MarketPlugin.Base
{
    public partial class Currency
    {
        public static Currency RUB => new Currency(Name: "Russian Ruble", ISO: "RUB");
        public static Currency USD => new Currency(Name: "US Dollar", ISO: "USD");
        public static Currency EUR => new Currency(Name: "Euro", ISO: "EUR");
        public static Currency BTC => new Currency(Name: "Bitcoin", ISO: "BTC");
        public static Currency BCH => new Currency(Name: "Bitcoin Cash", ISO: "BCH");
        public static Currency ETH => new Currency(Name: "Ethereum", ISO: "ETH");
    }
}
