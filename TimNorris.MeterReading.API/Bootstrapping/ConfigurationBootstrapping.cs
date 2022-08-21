
using TimNorris.MeterReadings.Application.Configuration;

namespace TimNorris.MeterReading.API.Bootstrapping
{
    internal static class ConfigurationBootstrapping
    {
        public static IServiceCollection BootstrapConfiguration(
            this IServiceCollection services,
            ConfigurationManager configurationManager
        ) =>
            services
                .AddSingleton<IApplicationOptions, ApplicationOptions>(provider => new ApplicationOptions());
    }
}
