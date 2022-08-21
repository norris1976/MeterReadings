namespace TimNorris.MeterReadings.Domain.Commands.Responses
{
    public class UploadMeterReadingsCommandResponse
    {
        public UploadMeterReadingsCommandResponse(int successful, int failed)
        {
            Successful = successful;
            Failed = failed;
        }

        public int Successful { get; }
        public int Failed { get; }
    }
}
