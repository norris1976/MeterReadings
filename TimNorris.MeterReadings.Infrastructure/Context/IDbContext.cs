using Microsoft.EntityFrameworkCore;
using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Infrastructure.Context
{
    public interface IDbContext
    {
        DbSet<MeterReading> MeterReadings { get; set; }
        DbSet<CustomerAccount> CustomerAccounts { get; set; }

        Task<int> Save();
    }
}
