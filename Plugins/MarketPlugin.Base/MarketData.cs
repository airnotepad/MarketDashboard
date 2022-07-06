namespace MarketPlugin.Base
{
    public class MarketData : IMarketData
    {
        public bool IsSuccessfull { get; set; }

        public IMarketResponse Response { get; set; }
        public Exception Exception { get; set; }
        public bool HasException => Exception is not null;
        public IDictionary<Pair, Price> Data { get; set; }
    }
}
