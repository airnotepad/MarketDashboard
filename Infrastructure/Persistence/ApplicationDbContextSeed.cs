using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(IApplicationDbContext context)
        {
            if (!context.Currencies.Any())
            {
                context.Currencies.AddRange(new[]
                {
                    new Currency() { Name = "Russian ruble", ISO = "RUB" },
                    new Currency() { Name = "United States dollar", ISO = "USD" },
                    new Currency() { Name = "Euro", ISO = "EUR" },
                    new Currency() { Name = "Bitcoin", ISO = "BTC" },
                    new Currency() { Name = "Ethereum", ISO = "ETH" },
                    new Currency() { Name = "BitcoinCash", ISO = "BCH" },
                });

                await context.SaveChangesAsync(CancellationToken.None);
            }

            if (!context.Markets.Any())
            {
                context.Markets.AddRange(new[]
                {
                    new Market() { Name = "Blockchain", Guid = new Guid("b4aff428-9db0-3a69-a9e5-e3f3b3a182a9"), IsActivate = true },
                    new Market() { Name = "Bestchange", Guid = new Guid("fd932762-ee8a-3149-adc9-fcb3c98644e3"), IsActivate = true },
                });

                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
