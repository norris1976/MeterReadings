using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Domain.Extensions
{
    public static class MeterReadingExtensions
    {
        public static MeterReading ToMeterReading(this MeterReadingFileRecord source) =>
            new MeterReading
            {
                AccountId = int.Parse(source.AccountId),
                MeterReadingDateTime = DateTimeOffset.Parse(source.MeterReadingDateTime),
                MeterReadValue = int.Parse(source.MeterReadValue),
                Id = Guid.NewGuid()
            };
    }
}
