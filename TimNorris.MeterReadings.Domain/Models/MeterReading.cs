using System.ComponentModel.DataAnnotations;

namespace TimNorris.MeterReadings.Domain.Models
{
    public class MeterReading
    {
        [Key]
        public Guid Id { get; set; }
        
        public int AccountId { get; set; }

        public DateTimeOffset MeterReadingDateTime { get; set; }
        
        public int MeterReadValue { get; set; }
    }
}