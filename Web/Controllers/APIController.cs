using Application.RatesExchanges.Rates.Currencies.Queries;
using Application.RatesExchanges.Rates.RateHistory.Queries;
using Application.RatesExchanges.Rates.Rate.Queries;
using Application.RatesExchanges.Markets.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;
        private readonly ISender _mediator;

        public APIController(ILogger<APIController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<CurrenciesVm>> Currencies()
        {
            return await _mediator.Send(new GetCurrenciesQuery());
        }

        [HttpGet]
        public async Task<ActionResult<RateVm>> Rate(string from, string to)
        {
            return await _mediator.Send(new GetRateQuery() { FromIso = from, ToIso = to });
        }

        [HttpGet]
        public async Task<ActionResult<RateHistoryVm>> RateHistory(string from, string to)
        {
            return await _mediator.Send(new GetRateHistoryQuery() { FromIso = from, ToIso = to });
        }

        [HttpGet]
        public async Task<ActionResult<MarketsVm>> Markets()
        {
            return await _mediator.Send(new GetMarketsQuery());
        }
    }
}