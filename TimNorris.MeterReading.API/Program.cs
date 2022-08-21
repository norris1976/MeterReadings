using Microsoft.OpenApi.Models;
using TimNorris.MeterReading.API.Bootstrapping;
using TimNorris.MeterReading.API.Extensions;

namespace TimNorris.MeterReading.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Meter Readings API",
                    Description = "Managing Meter Readings",
                    Version = "v1"
                });
            });

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();

            app.SeedData();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meter Readings API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}