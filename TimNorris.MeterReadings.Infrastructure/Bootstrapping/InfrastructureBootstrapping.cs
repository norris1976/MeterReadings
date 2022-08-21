using Microsoft.Extensions.DependencyInjection;

namespace TimNorris.MeterReadings.Infrastructure.Bootstrapping
{
    public static class InfrastructureBootstrapping
    {
        public static IServiceCollection BootstrapInfrastructure(this IServiceCollection services) =>
            services
                .BootstrapData();

    }
}
