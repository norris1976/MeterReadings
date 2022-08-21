using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Infrastructure.Context;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.LocalDb.Bootstrapping
{
    public static class LocalDbBootstrapping
    {
        public static IServiceCollection BootstrapLocalDb(
            this IServiceCollection services, string connectionString
        ) =>
            services
                .BootstrapDatabase(connectionString)
                .AddScoped<IDbContext, LocalDbContext>();
    }
}
