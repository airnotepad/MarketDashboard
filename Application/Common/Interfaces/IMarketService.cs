using MarketPlugin.Base;

namespace Application.Common.Interfaces
{
    public interface IMarketService
    {
        public void RefreshPlugins();
        public IList<string> GetMarketNames();
        public bool TryGetMarket(string Name, out IMarket Market);
        public bool TryGetMarket(Guid Guid, out IMarket Market);
    }
}
