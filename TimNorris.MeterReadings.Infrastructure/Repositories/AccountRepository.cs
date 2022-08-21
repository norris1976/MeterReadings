using TimNorris.MeterReadings.Application.Repositories;
using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.Infrastructure.Context;

namespace TimNorris.MeterReadings.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContext _dbContext;

        public AccountRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(CustomerAccount account)
        {
            _dbContext.CustomerAccounts.Add(account);
            _dbContext.Save();
        }
    }
}
