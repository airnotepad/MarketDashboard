using Application.Common.Mappings;

namespace Application.RatesExchanges.Rates.RateHistory.Queries
{
    public class RateHistoryDto : IMapFrom<Domain.Entities.Rate>
    {
        public DateTime AvailableAt { get; set; }
        public double Price { get; set; }
    }
}
