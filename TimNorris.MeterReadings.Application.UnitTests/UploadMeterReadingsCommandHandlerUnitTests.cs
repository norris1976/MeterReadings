using Moq;
using TimNorris.MeterReadings.Application.CommandHandlers;
using TimNorris.MeterReadings.Application.Queries;
using TimNorris.MeterReadings.Application.Repositories;
using TimNorris.MeterReadings.Application.Services;
using TimNorris.MeterReadings.Domain.Commands;
using TimNorris.MeterReadings.Domain.Models;
using Xunit;

namespace TimNorris.MeterReadings.Application.UnitTests
{
    public class UploadMeterReadingsCommandHandlerUnitTests
    {
        [Fact]
        public async Task Given_NoFileReadings_When_HandleCalled_Then_ReturnsZeroSuccessesAndFails()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mockAccountQuery = new Mock<IAccountQuery>();

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord>())
                .Verifiable();

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(0, response.Successful);
            Assert.Equal(0, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Never);
        }

        [Fact]
        public async Task Given_OneGoodFileReadings_When_HandleCalled_Then_ReturnsOneSuccessAndNoFails()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord> 
                {
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = "26/05/2019 09:24", MeterReadValue = "12345"  }
                })
                .Verifiable();

            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            mockMeterReadingQuery
                .Setup(m => m.GetByAccountIdAndDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .Returns(null as MeterReading);

            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository
                .Setup(m => m.Add(It.IsAny<MeterReading>()))
                .Verifiable();

            var mockAccountQuery = new Mock<IAccountQuery>();
            mockAccountQuery.Setup(m => m.AccountExists(It.IsAny<int>())).Returns(true);

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(1, response.Successful);
            Assert.Equal(0, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Once);
        }

        [Fact]
        public async Task Given_OneGoodFileReadingAndOneBad_When_HandleCalled_Then_ReturnsOneSuccessAndOneFail()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord>
                {
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = "26/05/2019 09:24", MeterReadValue = "12345"  },
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = "26/05/2019 09:24", MeterReadValue = "abcde"  }
                })
                .Verifiable();

            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            mockMeterReadingQuery
                .Setup(m => m.GetByAccountIdAndDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .Returns(null as MeterReading);

            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository
                .Setup(m => m.Add(It.IsAny<MeterReading>()))
                .Verifiable();

            var mockAccountQuery = new Mock<IAccountQuery>();
            mockAccountQuery.Setup(m => m.AccountExists(It.IsAny<int>())).Returns(true);

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(1, response.Successful);
            Assert.Equal(1, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Once);
        }

        [Fact]
        public async Task Given_OneGoodFileReadingAndExistingEarlier_When_HandleCalled_Then_ReturnsOneSuccess()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord>
                {
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = DateTimeOffset.UtcNow.ToString(), MeterReadValue = "12345"  },
                })
                .Verifiable();

            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            mockMeterReadingQuery
                .Setup(m => m.GetByAccountIdAndDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .Returns(new MeterReading { AccountId = 1234, MeterReadValue = 12345, Id = Guid.NewGuid(), MeterReadingDateTime = DateTimeOffset.UtcNow.Date });

            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository
                .Setup(m => m.Add(It.IsAny<MeterReading>()))
                .Verifiable();

            var mockAccountQuery = new Mock<IAccountQuery>();
            mockAccountQuery.Setup(m => m.AccountExists(It.IsAny<int>())).Returns(true);

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(1, response.Successful);
            Assert.Equal(0, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Once);
        }

        [Fact]
        public async Task Given_OneGoodFileReadingAndNoAccount_When_HandleCalled_Then_ReturnsOneFail()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord>
                {
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = DateTimeOffset.UtcNow.ToString(), MeterReadValue = "12345"  },
                })
                .Verifiable();

            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            mockMeterReadingQuery
                .Setup(m => m.GetByAccountIdAndDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .Returns(new MeterReading { AccountId = 1234, MeterReadValue = 12345, Id = Guid.NewGuid(), MeterReadingDateTime = DateTimeOffset.UtcNow.AddHours(1) });

            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository
                .Setup(m => m.Add(It.IsAny<MeterReading>()))
                .Verifiable();

            var mockAccountQuery = new Mock<IAccountQuery>();
            mockAccountQuery.Setup(m => m.AccountExists(It.IsAny<int>())).Returns(false);

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(0, response.Successful);
            Assert.Equal(1, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Never);
        }

        [Fact]
        public async Task Given_OneGoodFileReadingAndExistingReadingLater_When_HandleCalled_Then_ReturnsOneFailure()
        {
            // Arrange
            var mockFileProcessingService = new Mock<IFileProcessingService>();
            mockFileProcessingService
                .Setup(m => m.ParseCsvContent<MeterReadingFileRecord>(
                    It.IsAny<string>())
                )
                .ReturnsAsync(new List<MeterReadingFileRecord>
                {
                    new MeterReadingFileRecord{ AccountId = "1234", MeterReadingDateTime = DateTimeOffset.UtcNow.ToString(), MeterReadValue = "12345"  },
                })
                .Verifiable();

            var mockMeterReadingQuery = new Mock<IMeterReadingQuery>();
            mockMeterReadingQuery
                .Setup(m => m.GetByAccountIdAndDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>()))
                .Returns(new MeterReading 
                { 
                    AccountId = 1234, 
                    MeterReadValue = 12345, 
                    Id = Guid.NewGuid(), 
                    MeterReadingDateTime = DateTimeOffset.UtcNow.AddHours(1) 
                });

            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository
                .Setup(m => m.Add(It.IsAny<MeterReading>()))
                .Verifiable();

            var mockAccountQuery = new Mock<IAccountQuery>();
            mockAccountQuery.Setup(m => m.AccountExists(It.IsAny<int>())).Returns(true);

            string json = "test json";
            var request = new UploadMeterReadingsCommand(json);

            var handler = new UploadMeterReadingsCommandHandler(
                mockFileProcessingService.Object,
                mockMeterReadingQuery.Object,
                mockMeterReadingRepository.Object,
                mockAccountQuery.Object);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(0, response.Successful);
            Assert.Equal(1, response.Failed);
            mockMeterReadingRepository.Verify(m => m.Add(It.IsAny<MeterReading>()), Times.Never);
        }
    }
}
