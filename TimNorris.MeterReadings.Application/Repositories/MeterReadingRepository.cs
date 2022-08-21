using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.Application.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly LocalDbContext _dbContext;

        public MeterReadingRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MeterReading reading)
        {
            _dbContext.MeterReadings.Add(reading);
            _dbContext.SaveChanges();
        }
    }
}
