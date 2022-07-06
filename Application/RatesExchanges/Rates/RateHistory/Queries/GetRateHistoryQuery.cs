using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesExchanges.Rates.RateHistory.Queries
{
    public class GetRateHistoryQuery : IRequest<RateHistoryVm>
    {
        public string FromIso { get; set; }
        public string ToIso { get; set; }
    }

    public class GetRateHistoryQueryHandler : IRequestHandler<GetRateHistoryQuery, RateHistoryVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRateHistoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RateHistoryVm> Handle(GetRateHistoryQuery request, CancellationToken cancellationToken)
        {
            return new RateHistoryVm
            {
                Lists = await _context.Rates
                    .AsNoTracking()
                    .Where(r => r.CurrencyFrom == request.FromIso && r.CurrencyTo == request.ToIso)
                    .OrderBy(t => t.AvailableAt)
                    .ProjectTo<RateHistoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
