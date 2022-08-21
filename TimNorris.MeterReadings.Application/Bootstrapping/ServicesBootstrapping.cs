using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Application.Services;

namespace TimNorris.MeterReadings.Application.Bootstrapping
{
    internal static class ServicesBootstrapping
    {
        public static IServiceCollection BootstrapServices(this IServiceCollection services) =>
            services
                .AddScoped<IFileProcessingService, FileProcessingService>();
    }
}
