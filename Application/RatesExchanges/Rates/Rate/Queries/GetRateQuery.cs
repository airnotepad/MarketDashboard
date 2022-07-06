using Application.Common.Interfaces;
using Application.RatesExchanges.Rates.RateHistory.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RatesExchanges.Rates.Rate.Queries
{
    public class GetRateQuery : IRequest<RateVm>
    {
        public string FromIso { get; set; }
        public string ToIso { get; set; }
    }

    public class GetCurrenciesQueryHandler : IRequestHandler<GetRateQuery, RateVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrenciesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RateVm> Handle(GetRateQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rates.LastOrDefaultAsync(r => r.CurrencyFrom == request.FromIso && r.CurrencyTo == request.ToIso);

            return new RateVm
            {
                Data = _mapper?.Map<RateDto>(entity)
            };
        }
    }
}
