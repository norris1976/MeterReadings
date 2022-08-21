namespace TimNorris.MeterReadings.Application.Services
{
    public interface IFileProcessingService
    {
        Task<IEnumerable<T>> ParseCsvContent<T>(string content);
    }
}
