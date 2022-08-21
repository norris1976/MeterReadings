using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Infrastructure.Context;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.LocalDb.Bootstrapping
{
    public static class DatabaseBootstrapping
    {
#pragma warning disable CS8603 // Possible null reference return.
        private static string AssemblyName => typeof(DatabaseBootstrapping).Assembly.FullName;
#pragma warning restore CS8603 // Possible null reference return.

        public static IServiceCollection BootstrapDatabase(
            this IServiceCollection services,
            string connectionString
        )
        {
            void BuildDatabaseOptions(DbContextOptionsBuilder options)
            {
                options.UseSqlite(connectionString);
            }

            return services.AddDbContext<LocalDbContext>(BuildDatabaseOptions);
        }
    }
}
