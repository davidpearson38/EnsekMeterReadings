namespace EnsekMeterReadings.Business.CsvParsing
{
    using EnsekMeterReadings.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MeterReadingCsvRowParser
    {
        public static MeterReadingCsvRowParseResult Parse(
            IReadOnlyCollection<Account> accounts,
            string meterReadingCsvRow)
        {
            var csvValues = meterReadingCsvRow.Split(",");

            if (csvValues.Length < 3)
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{meterReadingCsvRow} contains an insufficient number of values.");
            }
            
            var accountIdString = csvValues[0].Trim();

            if (!int.TryParse(accountIdString, out var accountId))
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{accountIdString} is not a valid Account ID.");
            }

            var account = accounts.SingleOrDefault(acc => acc.Id == accountId);

            if (account == null)
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{accountId} is not a recognised Account ID.");
            }

            var meterReadingDateTimeString = csvValues[1].Trim();

            if (!DateTime.TryParse(meterReadingDateTimeString, out var meterReadingDateTime))
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{meterReadingDateTimeString} is not a valid date/time.");
            }

            var valueString = csvValues[2].Trim();

            if (!int.TryParse(valueString, out var value))
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{valueString} is not a valid meter reading value.");
            }

            if (value < 0 || value > 99999)
            {
                return MeterReadingCsvRowParseResult.Failure(
                    $"{value} is not within the valid meter reading value range of 0-99999.");
            }

            var meterReading = new MeterReading(account, meterReadingDateTime, value);

            return MeterReadingCsvRowParseResult.Success(meterReading);
        }
    }
}
