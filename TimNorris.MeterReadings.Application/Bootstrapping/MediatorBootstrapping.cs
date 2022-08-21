using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TimNorris.MeterReadings.Application.CommandHandlers;

namespace TimNorris.MeterReadings.Application.Bootstrapping
{
    internal static class MediatorBootstrapping
    {
        public static IServiceCollection BootstrapMediator(this IServiceCollection services) =>
            services.AddMediatR(typeof(UploadMeterReadingsCommandHandler));
    }
}
