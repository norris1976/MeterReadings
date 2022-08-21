using Microsoft.Extensions.DependencyInjection;

namespace TimNorris.MeterReadings.Application.Bootstrapping
{
    public static class ApplicationBootstrapping
    {
        public static IServiceCollection BootstrapApplication(this IServiceCollection services)
            => services
                .BootstrapMediator()
                .BootstrapServices()
                .BootstrapData();
    }
}
