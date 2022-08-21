using Microsoft.EntityFrameworkCore;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.Infrastructure.Context;

namespace TimNorris.MeterReadings.LocalDb.Context
{
    public class LocalDbContext : DbContext, IDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> contextOptions) : base(contextOptions)
        {
            //Database.EnsureCreated();
        }

        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        public async Task<int> Save() =>
            await base.SaveChangesAsync();
    }
}
