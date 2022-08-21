using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.Infrastructure.Context;

namespace TimNorris.MeterReadings.Infrastructure.Queries
{
    public class MeterReadingQuery : IMeterReadingQuery
    {
        private readonly IDbContext _dbContext;

        public MeterReadingQuery(IDbContext dbContext)
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
