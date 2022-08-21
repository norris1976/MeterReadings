using MediatR;
using TimNorris.MeterReadings.Application.Extensions;
using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Application.Repositories;
using TimNorris.MeterReadings.Application.Services;
using TimNorris.MeterReadings.Domain.Commands;
using TimNorris.MeterReadings.Domain.Commands.Responses;
using TimNorris.MeterReadings.Domain.Extensions;
using TimNorris.MeterReadings.Domain.Models;

namespace TimNorris.MeterReadings.Application.CommandHandlers
{
    public class UploadMeterReadingsCommandHandler
        : IRequestHandler<UploadMeterReadingsCommand, UploadMeterReadingsCommandResponse>
    {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly IMeterReadingQuery _meterReadingQuery;
        private readonly IMeterReadingRepository _meterReadingRepository;
        private readonly IAccountQuery _accountQuery;

        public UploadMeterReadingsCommandHandler(IFileProcessingService fileProcessingService,
            IMeterReadingQuery meterReadingQuery,
            IMeterReadingRepository meterReadingRepository,
            IAccountQuery accountQuery)
        {
            _fileProcessingService = fileProcessingService;
            _meterReadingQuery = meterReadingQuery;
            _meterReadingRepository = meterReadingRepository;
            _accountQuery = accountQuery;
        }

        public async Task<UploadMeterReadingsCommandResponse> Handle(
            UploadMeterReadingsCommand request, 
            CancellationToken cancellationToken
        )
        {
            var readings = await _fileProcessingService.ParseCsvContent<MeterReadingFileRecord>(request.FileContent);

            var rejectedCount = 0;
            var successfulCount = 0;
            foreach (var fileReading in readings)
            {
                if(fileReading.IsValid())
                { 
                    var reading = fileReading.ToMeterReading();

                    if (_accountQuery.AccountExists(reading.AccountId))
                    {
                        // If no existing reading for that reading date
                        // AND 
                        // new reading is more recent that existing reading
                        var existingReading = _meterReadingQuery.GetByAccountIdAndDate(reading.AccountId, reading.MeterReadingDateTime);
                        if(reading.IsNewerThan(existingReading))
                        {
                            successfulCount++;
                            _meterReadingRepository.Add(reading);
                            continue;
                        }
                    }
                }
                rejectedCount++;
            }

            return new UploadMeterReadingsCommandResponse(successfulCount, rejectedCount);
        }
    }
}
