using MarketPlugin.Base;

namespace Bestchange
{
    class MarketAPI : IMarket
    {
        public string Name => "BestchangeAPI";

        public IList<Pair> Pairs => new List<Pair>()
        {
            new Pair(
                From: Currency.RUB,
                To: Currency.BTC),
            new Pair(
                From: Currency.USD,
                To: Currency.BTC),
            new Pair(
                From: Currency.EUR,
                To: Currency.BTC),
        };

        public async Task<IMarketData> GetDataAsync(string proxy)
        {
            var result = new MarketData();

            try
            {
                var client = new BestchangeClient(proxy);

                var response = await client.GetFilesAsync();
                result.Response = response;
                result.IsSuccessfull = client.IsSuccessful;

                client.GetExchangers();
                client.GetRates();

                result.Data = Parse(client.Rates);

                client.RemoveFiles();
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        private IDictionary<Pair, Price> Parse(List<BestChangeRate> data)
        {
            var result = new Dictionary<Pair, Price>();

            foreach (var pair in Pairs)
            {
                var rate = data.GetRate(pair);

                if (rate is not null)
                {
                    result.Add(pair, new Price(rate.rate));
                }
            }

            return result;
        }
    }
}
