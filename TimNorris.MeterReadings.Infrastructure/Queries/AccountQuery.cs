using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.Infrastructure.Context;

namespace TimNorris.MeterReadings.Infrastructure.Queries
{
    public class AccountQuery : IAccountQuery
    {
        private readonly IDbContext _dbContext;

        public AccountQuery(IDbContext dbContext)
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
