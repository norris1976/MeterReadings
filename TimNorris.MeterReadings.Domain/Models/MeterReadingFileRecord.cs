namespace TimNorris.MeterReadings.Domain.Models
{
    public class MeterReadingFileRecord
    {        
        public string? AccountId { get; set; }

        public string? MeterReadingDateTime { get; set; }
        
        public string? MeterReadValue { get; set; }
    }
}