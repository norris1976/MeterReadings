using MediatR;
using TimNorris.MeterReadings.Domain.Commands.Responses;

namespace TimNorris.MeterReadings.Domain.Commands
{
    public class UploadMeterReadingsCommand : IRequest<UploadMeterReadingsCommandResponse>
    {
        public UploadMeterReadingsCommand(string fileContent)
        {
            FileContent = fileContent;
        }

        public string FileContent { get; }
    }
}
