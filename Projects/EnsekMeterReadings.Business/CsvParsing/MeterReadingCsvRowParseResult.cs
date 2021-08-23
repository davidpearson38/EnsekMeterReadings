using EnsekMeterReadings.Domain;

namespace EnsekMeterReadings.Business.CsvParsing
{
    public class MeterReadingCsvRowParseResult
    {
        private MeterReadingCsvRowParseResult(
            string error,
            MeterReading meterReading)
        {
            this.Error = error;
            this.MeterReading = meterReading;
        }

        public string Error { get; }

        public bool IsSuccess => string.IsNullOrEmpty(this.Error);

        public MeterReading MeterReading { get; }

        public static MeterReadingCsvRowParseResult Success(MeterReading meterReading)
        {
            return new MeterReadingCsvRowParseResult(null, meterReading);
        }

        public static MeterReadingCsvRowParseResult Failure(string error)
        {
            return new MeterReadingCsvRowParseResult(error, null);
        }
    }
}
