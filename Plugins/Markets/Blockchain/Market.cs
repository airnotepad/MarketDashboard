using Common.RestSharp;
using MarketPlugin.Base;
using Newtonsoft.Json;
using RestSharp;

namespace Blockchain
{
    public class Market : IMarket
    {
        public string Name => "Blockchain";
        private string Url => "https://blockchain.info/ticker";
        private string UserAgent => "";
        private Method Method => Method.Get;

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
                var client = new RestClient();
                client.Options.UserAgent = UserAgent;
                client.SetProxy(proxy);

                var request = new RestRequest(Url, Method);

                var response = await client.ExecuteAsync(request);

                result.IsSuccessfull = response.IsSuccessful;

                var marketResponse = new MarketResponse();

                marketResponse.StatusCode = response.StatusCode;
                marketResponse.Content = response.Content;
                marketResponse.ErrorMessage = response.ErrorMessage;
                marketResponse.Exception = response.ErrorException;
                result.Response = marketResponse;

                if (response.IsSuccessful)
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, BlockchainData>>(response.Content);
                    result.Data = Parse(data);
                }

            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        private IDictionary<Pair, Price> Parse(Dictionary<string, BlockchainData> data)
        {
            var result = new Dictionary<Pair, Price>();

            foreach (var pair in Pairs)
            {
                if (data.TryGetValue(pair.From.ISO, out var price))
                {
                    result.Add(pair, new Price(price.Last));
                }
            }

            return result;
        }
    }
}
