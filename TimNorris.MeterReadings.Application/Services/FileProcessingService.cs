using CsvHelper;
using System.Globalization;
using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Services
{
    public class FileProcessingService : IFileProcessingService
    {
        public Task<IEnumerable<T>> ParseCsvContent<T>(string content)
        {
            using (var reader = new StringReader(content))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<T>().ToList();
                return Task.FromResult(records.AsEnumerable());
            }
        }
    }
}
