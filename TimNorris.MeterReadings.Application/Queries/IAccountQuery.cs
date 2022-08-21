using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Queries
{
    public interface IAccountQuery
    {
        CustomerAccount GetById(int accountId);
        bool AccountExists(int accountId);
    }
}
