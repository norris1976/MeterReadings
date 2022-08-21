using TimNorris.MeterReadings.Domain.Models;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.Application.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LocalDbContext _dbContext;

        public AccountRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(CustomerAccount account)
        {
            _dbContext.CustomerAccounts.Add(account);
            _dbContext.SaveChanges();
        }
    }
}
