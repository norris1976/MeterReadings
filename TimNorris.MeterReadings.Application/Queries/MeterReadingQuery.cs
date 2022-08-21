using Microsoft.EntityFrameworkCore;
using TimNorris.MeterReadings.Application.Configuration;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.Application.Queries
{
    public class MeterReadingQuery : IMeterReadingQuery
    {
        private readonly LocalDbContext _dbContext;

        public MeterReadingQuery(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MeterReading GetByAccountIdAndDate(int accountId, DateTimeOffset date)
        {
            var sameDayReadings = _dbContext.MeterReadings
                .Where(r => r.AccountId == accountId && r.MeterReadingDateTime == date).ToList();

            return sameDayReadings.OrderByDescending(r => r.MeterReadingDateTime).FirstOrDefault();
        }
    }
}
