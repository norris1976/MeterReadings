
using TimNorris.MeterReading.API.Filters;
using TimNorris.MeterReadings.Application.Configuration;

namespace TimNorris.MeterReading.API.Bootstrapping
{
    internal static class ValidationBootstrapping
    {
        public static IServiceCollection BootstrapValidation(this IServiceCollection services) =>
            services
                .AddScoped<AllowedExtensionsFilterAttribute>();
    }
}
