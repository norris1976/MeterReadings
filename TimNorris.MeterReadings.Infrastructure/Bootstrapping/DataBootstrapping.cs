using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Application.Repositories;
using TimNorris.MeterReadings.Infrastructure.Queries;
using TimNorris.MeterReadings.Infrastructure.Repositories;

namespace TimNorris.MeterReadings.Infrastructure.Bootstrapping
{
    internal static class DataBootstrapping
    {
        public static IServiceCollection BootstrapData(this IServiceCollection services) =>
            services
                .AddScoped<IMeterReadingQuery, MeterReadingQuery>()
                .AddScoped<IMeterReadingRepository, MeterReadingRepository>()
                .AddScoped<IAccountQuery, AccountQuery>()
                .AddScoped<IAccountRepository, AccountRepository>();
    }
}
