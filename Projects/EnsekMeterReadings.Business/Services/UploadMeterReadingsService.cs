namespace EnsekMeterReadings.Business.Services
{
    using EnsekMeterReadings.Business.CsvParsing;
    using EnsekMeterReadings.Domain;
    using EnsekMeterReadings.Domain.Abstractions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UploadMeterReadingsService : IUploadMeterReadingsService
    {
        private readonly IMeterReadingsRepository meterReadingsRepository;

        public UploadMeterReadingsService(
            IMeterReadingsRepository meterReadingsRepository)
        {
            this.meterReadingsRepository = meterReadingsRepository;
        }

        public async Task<MeterReadingsUploadResponse> UploadAsync(string meterReadings)
        {
            var accounts = await this.meterReadingsRepository.GetAllAccountsAsync();

            const int HeaderRows = 1;

            var meterReadingCsvRows = meterReadings
                .Replace("\r\n", "\r")
                .Split('\r')
                .Skip(HeaderRows);

            var results = new List<MeterReadingCsvRowParseResult>();

            foreach (var meterReadingCsvRow in meterReadingCsvRows)
            {
                var result = MeterReadingCsvRowParser.Parse(accounts, meterReadingCsvRow);

                if (result.IsSuccess)
                {
                    // Should not be able to load the same entry twice, but not sure what an "entry" is:
                    // a) duplicate account IDs within the upload.
                    // b) duplicate account IDs/datetimes within the upload.
                    // c) existing entry for that account ID in the DB already?
                    // d) existing entry for that account ID/datetime in the DB already?
                    // e) or is it existing/duplicate entry for the calendar date (indicated by the date/time)?
                    await this.meterReadingsRepository.AddMeterReadingAsync(result.MeterReading);
                }

                results.Add(result);
            }

            return new MeterReadingsUploadResponse(
                results.Where(r => r.IsSuccess).Count(),
                results.Where(r => !r.IsSuccess).Select(r => r.Error).ToArray());
        }
    }
}
