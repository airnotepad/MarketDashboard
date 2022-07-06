using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Rate> Rates { get; set; }

        DbSet<Currency> Currencies { get; set; }

        DbSet<Market> Markets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
