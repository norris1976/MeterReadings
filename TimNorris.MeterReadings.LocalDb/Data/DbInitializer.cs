using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimNorris.MeterReadings.LocalDb.Context;

namespace TimNorris.MeterReadings.LocalDb.Data
{
    public class DbInitializer
    {
        private readonly LocalDbContext _db;

        public DbInitializer(LocalDbContext db)
        {
            _db = db;
        }

        public void Seed()
        {
            _db.Database.EnsureCreated();

            if(!_db.CustomerAccounts.Any())
            {
                var seeder = new AccountSeeder(_db);

                seeder.Seed();
            }
        }
    }
}
