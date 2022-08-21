using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.Application.Queries
{
    public class AccountQuery : IAccountQuery
    {
        private readonly LocalDbContext _dbContext;

        public AccountQuery(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerAccount GetById(int accountId)
        {
            return _dbContext.CustomerAccounts.FirstOrDefault(a => a.AccountId == accountId);
        }

        public bool AccountExists(int accountId)
        {
            return _dbContext.CustomerAccounts.Any(a => a.AccountId == accountId);
        }
    }
}
