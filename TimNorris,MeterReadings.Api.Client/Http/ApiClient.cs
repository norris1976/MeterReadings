using RestSharp;
using TimNorris_MeterReadings.Api.Client.Requests;
using TimNorris_MeterReadings.Api.Client.Responses;

namespace TimNorris_MeterReadings.Api.Client.Http
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl) =>
            _client = new RestClient(baseUrl);

        public async Task<UploadMeterReadingsResponse> UploadMeterReadings(UploadMeterReadingsRequest request)
        {
            return await _client.PostJsonAsync<UploadMeterReadingsRequest, UploadMeterReadingsResponse>(
                "meterreadings/meter-reading-uploads", request, CancellationToken.None
            );
        }
    }
}
