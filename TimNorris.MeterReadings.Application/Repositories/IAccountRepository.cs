using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Repositories
{
    public interface IAccountRepository
    {
        void Add(CustomerAccount account);
    }
}
