using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.RatesExchanges.Rates.Currencies.Queries
{
    public class CurrencyDto : IMapFrom<Currency>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Currency, CurrencyDto>()
                .ForMember(d => d.ShortName, opt => opt.MapFrom(s => s.ISO));
        }
    }
}
