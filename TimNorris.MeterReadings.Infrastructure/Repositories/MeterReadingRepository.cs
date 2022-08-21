using TimNorris.MeterReadings.Application.Repositories;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.Infrastructure.Context;

namespace TimNorris.MeterReadings.Infrastructure.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly IDbContext _dbContext;

        public MeterReadingRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MeterReading reading)
        {
            _dbContext.MeterReadings.Add(reading);
            _dbContext.Save();
        }
    }
}
