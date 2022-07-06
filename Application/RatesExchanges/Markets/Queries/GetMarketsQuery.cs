using Application.Common.Interfaces;
using MediatR;

namespace Application.RatesExchanges.Markets.Queries
{
    public class GetMarketsQuery : IRequest<MarketsVm>
    {

    }

    public class GetMarketsQueryHandler : IRequestHandler<GetMarketsQuery, MarketsVm>
    {
        private readonly IMarketService _marketService;

        public GetMarketsQueryHandler(IMarketService marketService)
        {
            _marketService = marketService;
        }

        public async Task<MarketsVm> Handle(GetMarketsQuery request, CancellationToken cancellationToken)
        {
            var marketNames = _marketService.GetMarketNames();

            var list = new List<MarketDto>();
            foreach (var marketName in marketNames)
            {
                var marketVersion = string.Empty;
                var marketGuid = string.Empty;
                var marketFile = string.Empty;
                if (_marketService.TryGetMarket(marketName, out var market))
                {
                    var marketType = market.GetType();
                    marketVersion = marketType.Assembly.GetName().Version.ToString();
                    marketGuid = marketType.GUID.ToString();
                    marketFile = new FileInfo(marketType.Assembly.Location).Name;
                }
                else
                {
                    marketVersion = "Error of getting market dll";
                    marketGuid = "Error of getting market dll";
                    marketFile = "Error of getting market dll";
                }

                list.Add(new MarketDto()
                {
                    Name = marketName,
                    Version = marketVersion,
                    Guid = marketGuid,
                    FileName = marketFile
                });
            }

            return new MarketsVm() { Markets = list };
        }
    }
}
