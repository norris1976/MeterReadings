using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TimNorris.MeterReadings.LocalDb.Context;
using CsvHelper;
using System.Globalization;
using System.Reflection.Metadata;
using TimNorris.MeterReadings.Domain.Models;
using System.IO;
using System.Reflection;

namespace TimNorris.MeterReadings.LocalDb.Data
{
    public class AccountSeeder
    {
        private readonly LocalDbContext _db;
        public AccountSeeder(LocalDbContext db)
        {
            _db = db;
        }
        private IList<CustomerAccount> GetData()
        {
            var assembly = typeof(LocalDbContext).Assembly;
            var stream = assembly.GetManifestResourceStream("TimNorris.MeterReadings.LocalDb.Data.Test_Accounts.csv");
            if (stream == null)
            {
                throw new FileNotFoundException("Cannot find mappings seeding embedded resource.");
            }

            using (var r = new StreamReader(stream))
            {
                using (var csv = new CsvReader(r, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CustomerAccount>().ToList();
                    return records;
                }
            }
        }
        public void Seed()
        {
            var accounts = GetData();
            foreach (var item in accounts)
            {
                _db.CustomerAccounts.Add(item);
            }
            _db.SaveChanges();
        }
    }
}
