namespace MarketPlugin.Base
{
    public interface IMarketData
    {
        public bool IsSuccessfull { get; }
        public IMarketResponse Response { get; }
        public Exception Exception { get; }
        public bool HasException { get; }
        public IDictionary<Pair, Price> Data { get; }
    }
}
