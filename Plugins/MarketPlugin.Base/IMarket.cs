namespace MarketPlugin.Base
{
    public interface IMarket
    {
        public string Name { get; }
        public Task<IMarketData> GetDataAsync(string proxy);
        public IList<Pair> Pairs { get; }
    }
}
