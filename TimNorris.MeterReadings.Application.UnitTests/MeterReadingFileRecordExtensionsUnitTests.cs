using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimNorris.MeterReadings.Application.Extensions;
using TimNorris.MeterReadings.Domain.Models;
using Xunit;

namespace TimNorris.MeterReadings.Application.UnitTests
{
    public class MeterReadingFileRecordExtensionsUnitTests
    {
        [Fact]
        public async Task Given_OldMeterReadingComparedToNew_When_IsNewerThan_Then_ReturnsFalse()
        {
            // Arrange
            var oldMeterReading = new MeterReading { MeterReadingDateTime = DateTimeOffset.UtcNow.AddHours(-1) };
            var newMeterReading = new MeterReading { MeterReadingDateTime = DateTimeOffset.UtcNow };

            // Act
            var result = oldMeterReading.IsNewerThan(newMeterReading);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Given_NewMeterReadingCompartedToOld_When_IsNewerThan_Then_ReturnsTrue()
        {
            // Arrange
            var oldMeterReading = new MeterReading { MeterReadingDateTime = DateTimeOffset.UtcNow.AddHours(-1) };
            var newMeterReading = new MeterReading { MeterReadingDateTime = DateTimeOffset.UtcNow };

            // Act
            var result = newMeterReading.IsNewerThan(oldMeterReading);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("1234", "12345", "26/05/2019 09:24", true)]
        [InlineData("1234q", "12345", "26/05/2019 09:24", false)]
        [InlineData("1234", "1234", "26/05/2019 09:24", false)]
        [InlineData("1234", "12345", "", false)]
        public async Task Given_MeterReadingFileRecord_HasInvalidAccountId_When_IsValidCalled_Then_ReturnsFalse(
            string accountId,
            string readingValue,
            string date,
            bool expectedResult
            )
        {
            // Arrange
            var meterReadingRecord = new MeterReadingFileRecord 
            { 
                AccountId = accountId,
                MeterReadValue = readingValue,
                MeterReadingDateTime = date
            };

            // Act
            var result = meterReadingRecord.IsValid();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}