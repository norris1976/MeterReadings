using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TimNorris.MeterReading.API.Filters;
using TimNorris.MeterReadings.Domain.Commands;
using TimNorris_MeterReadings.Api.Client.Requests;
using TimNorris_MeterReadings.Api.Client.Responses;

namespace TimNorris.MeterReading.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingsController : ControllerBase
    {
        private readonly ILogger<MeterReadingsController> _logger;
        private readonly IMediator _mediator;

        public MeterReadingsController(ILogger<MeterReadingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("meter-reading-uploads")]
        [Consumes("multipart/form-data")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1048576)]
        [RequestSizeLimit(1048576)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowedExtensionsFilter(Extensions = new string[] {".csv"})]

        public async Task<ActionResult<UploadMeterReadingsResponse>> UploadMeterReadings([FromForm] UploadMeterReadingsRequest request)
        {
            _logger.LogDebug($"Uploading Meter Readings.");

            if (request == null || request.File == null)
                return BadRequest();

            try
            {
                var content = new StringBuilder();

                using (var reader = new StreamReader(request.File.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        content.AppendLine(reader.ReadLine());
                }

                var response = await _mediator.Send(new UploadMeterReadingsCommand(content.ToString()));

                return response == null
                    ? StatusCode(StatusCodes.Status422UnprocessableEntity)
                    : Ok(new UploadMeterReadingsResponse { SuccessfulReadings = response.Successful, FailedReadings = response.Failed });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error reading file input");

                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}