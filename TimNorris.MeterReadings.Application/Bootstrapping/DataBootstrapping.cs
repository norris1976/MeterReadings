using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Application.Repositories;

namespace TimNorris.MeterReadings.Application.Bootstrapping
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
