using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesExchanges.Rates.Currencies.Queries
{
    public class GetCurrenciesQuery : IRequest<CurrenciesVm>
    {
    }

    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, CurrenciesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrenciesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CurrenciesVm> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return new CurrenciesVm
            {
                Lists = await _context.Currencies
                    .AsNoTracking()
                    .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
