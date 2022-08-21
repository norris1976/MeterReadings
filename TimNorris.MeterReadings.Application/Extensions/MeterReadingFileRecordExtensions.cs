using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.Extensions
{
    public static class MeterReadingFileRecordExtensions
    {
        public static bool IsValid(this MeterReadingFileRecord source)
        {
            // Check for valid values
            if (source == null || 
                    source.MeterReadValue == null || 
                    source.AccountId == null || 
                    source.MeterReadingDateTime == null
                )
                return false;

            // Account ID should be numeric value
            if (!int.TryParse(source.AccountId, out var accountValue))
                return false;

            // Reading should be numeric value
            if (!int.TryParse(source.MeterReadValue, out var readingValue))
                return false;

            // Acceptance Criteria: Reading should be format NNNNN (numeric and length 5)
            if (source.MeterReadValue.Length != 5)
                return false;

            // Timestamp should be a valid date
            if (!DateTimeOffset.TryParse(source.MeterReadingDateTime, out var readingDate))
                return false;

            return true;
        }

        public static bool IsNewerThan(this MeterReading source, MeterReading compareTo)
        {
            if (compareTo == null || source == null)
                return true;

            return source.MeterReadingDateTime > compareTo.MeterReadingDateTime;
        }

    }
}
