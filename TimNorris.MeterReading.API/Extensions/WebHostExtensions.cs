using TimNorris.MeterReadings.LocalDb.Context;
using TimNorris.MeterReadings.LocalDb.Data;

namespace TimNorris.MeterReading.API.Extensions
{
    public static class WebHostExtensions
    {
        public static WebApplication SeedData(this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<LocalDbContext>();
                    var env = services.GetRequiredService<IWebHostEnvironment>();
                    //if (env.IsDevelopment())
                    {
                        var seeder = new DbInitializer(context);
                        seeder.Seed();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred initializing the DB.");
                }
            }
            return host;
        }
    }
}
