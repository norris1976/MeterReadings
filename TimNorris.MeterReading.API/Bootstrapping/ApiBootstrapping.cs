using TimNorris.MeterReadings.Application.Bootstrapping;
using TimNorris.MeterReadings.LocalDb.Bootstrapping;

namespace TimNorris.MeterReading.API.Bootstrapping
{
    public static class ApiBootstrapping
    {
        public static IServiceCollection AddAllServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager
            ) =>
                services
                    .BootstrapLocalDb(configurationManager.GetConnectionString("LocalDbConnectionString"))
                    .BootstrapApplication()
                    .BootstrapValidation();
    }
}
