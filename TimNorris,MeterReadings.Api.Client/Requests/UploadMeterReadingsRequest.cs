using Microsoft.AspNetCore.Http;

namespace TimNorris_MeterReadings.Api.Client.Requests
{
    public class UploadMeterReadingsRequest
    {
        public IFormFile? File { get; set; }
    }
}
