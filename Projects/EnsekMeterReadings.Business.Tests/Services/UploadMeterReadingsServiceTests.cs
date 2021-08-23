namespace EnsekMeterReadings.Business.Services.Tests
{
    using EnsekMeterReadings.Business.Services;
    using EnsekMeterReadings.Domain;
    using EnsekMeterReadings.Domain.Abstractions;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UploadMeterReadingsServiceTests
    {
        [Test]
        public async Task Successfully_parsed_results_are_saved_and_included_in_response()
        {
            var validCsvRows =
                "AccountId,MeterReadingDateTime,MeterReadValue\r" +
                "1,22/04/2019 09:24,1002\r" +
                "2,22/04/2019 12:25,323";

            var mockRepository = new Mock<IMeterReadingsRepository>();
            mockRepository
                .Setup(r => r.GetAllAccountsAsync())
                .ReturnsAsync(
                    new[] { new Account(1, "Fred", "Bloggs"), new Account(2, "John", "Smith") });

            var service = new UploadMeterReadingsService(mockRepository.Object);

            var response = await service.UploadAsync(validCsvRows);

            Assert.That(response.NumberOfSuccesses, Is.EqualTo(2));
            Assert.That(response.Failures.Count(), Is.Zero);

            mockRepository
                .Verify(
                    r => r.AddMeterReadingAsync(It.Is<MeterReading>(
                        m => m.Account.Id == 1 &&
                        m.MeterReadingDateTime == new DateTime(2019, 04, 22, 09, 24, 00) &&
                        m.Value == 1002)),
                    Times.Once);
            mockRepository
                .Verify(
                    r => r.AddMeterReadingAsync(It.Is<MeterReading>(
                        m => m.Account.Id == 2 &&
                        m.MeterReadingDateTime == new DateTime(2019, 04, 22, 12, 25, 00) &&
                        m.Value == 323)),
                    Times.Once);
        }

        [Test]
        public async Task Failed_results_are_not_saved_and_included_in_response()
        {
            var validCsvRows =
                "AccountId,MeterReadingDateTime,MeterReadValue\r" +
                "BLAH,BLAH,BLAH\r" +
                "2,22/04/2019 12:25,323";

            var mockRepository = new Mock<IMeterReadingsRepository>();
            mockRepository
                .Setup(r => r.GetAllAccountsAsync())
                .ReturnsAsync(
                    new[] { new Account(1, "Fred", "Bloggs"), new Account(2, "John", "Smith") });

            var service = new UploadMeterReadingsService(mockRepository.Object);

            var response = await service.UploadAsync(validCsvRows);

            Assert.That(response.NumberOfSuccesses, Is.EqualTo(1));
            Assert.That(response.Failures.Count(), Is.EqualTo(1));

            mockRepository
                .Verify(
                    r => r.AddMeterReadingAsync(It.Is<MeterReading>(
                        m => m.Account.Id == 2 &&
                        m.MeterReadingDateTime == new DateTime(2019, 04, 22, 12, 25, 00) &&
                        m.Value == 323)),
                    Times.Once);

            // Once and only once
            mockRepository
                .Verify(
                    r => r.AddMeterReadingAsync(It.IsAny<MeterReading>()),
                    Times.Once);
        }
    }
}
