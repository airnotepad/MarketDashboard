using Application.Common.Mappings;

namespace Application.RatesExchanges.Rates.Rate.Queries
{
    public class RateDto : IMapFrom<Domain.Entities.Rate>
    {
        public DateTime AvailableAt { get; set; }
        public double Price { get; set; }
    }
}
