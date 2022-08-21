using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Repositories
{
    public interface IMeterReadingRepository
    {
        void Add(MeterReading reading);
    }
}
