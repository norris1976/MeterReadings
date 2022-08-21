using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Queries
{
    public interface IMeterReadingQuery
    {
        MeterReading GetByAccountIdAndDate(int accountId, DateTimeOffset date);
    }
}
