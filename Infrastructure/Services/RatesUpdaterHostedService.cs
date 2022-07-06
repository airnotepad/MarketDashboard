using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Services.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Infrastructure.Services
{
    class RatesUpdaterHostedService : ScheduledHostedServiceBase
    {
        readonly ILogger<RatesUpdaterHostedService> _logger;

        public RatesUpdaterHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<RatesUpdaterHostedService> logger)
            : base(serviceScopeFactory, logger)
        {
            _logger = logger;
        }

        protected override string Schedule => "*/30 * * * * *";
        protected override bool IncludingSeconds => true;
        protected override string DisplayName => "RatesUpdaterHostedService";

        protected override async Task ProcessInScopeAsync(IServiceProvider serviceProvider, CancellationToken token)
        {
            var _context = serviceProvider.GetService<IApplicationDbContext>();
            var _markets = serviceProvider.GetService<IMarketService>();

            string proxy = null;
            var selectedMarkets = await _context.Markets.Where(m => m.IsActivate).ToListAsync(token);

            var rateList = new ConcurrentBag<Rate>();

            if (selectedMarkets.Any())
                Parallel.ForEach(selectedMarkets, market =>
                {
                    if (_markets.TryGetMarket(market.Guid, out var implementation))
                    {
                        var data = implementation.GetDataAsync(proxy).Result;

                        if (data.HasException || data.Response.HasException)
                        {
                            var error = new Exception($"Get {market.Name} data error");
                            _logger.LogError(error, "{@Data}", data);
                            throw error;
                        }
                        else
                            if (data.IsSuccessfull && data.Data.Any())
                            foreach (var rate in data.Data)
                            {
                                rateList.Add(new Rate()
                                {
                                    AvailableAt = DateTime.Now,
                                    CurrencyFrom = rate.Key.From.ISO,
                                    CurrencyTo = rate.Key.To.ISO,
                                    Price = rate.Value.Value
                                });

                                _logger.LogInformation("New {Market} rate: {From} {To} {Price}",
                                    market.Name, rate.Key.From.ISO, rate.Key.To.ISO, rate.Value.Value);
                            }
                    }
                });

            if (rateList.Any())
            {
                _context.Rates.AddRange(rateList);
                await _context.SaveChangesAsync(token);
            }
        }
    }
}
