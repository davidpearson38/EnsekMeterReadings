namespace EnsekMeterReadings.Business.Tests.CsvParsing
{
    using EnsekMeterReadings.Business.CsvParsing;
    using EnsekMeterReadings.Domain;
    using NUnit.Framework;
    using System;

    public class MeterReadingCsvRowParserTests
    {
        [Test]
        public void Parsing_a_row_containing_a_valid_account_and_date_and_value_should_output_a_meter_reading()
        {
            const string CsvRow = "2345,22/04/2019 12:25,45522";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                CsvRow);

            Assert.That(string.IsNullOrEmpty(result.Error), Is.True);
            Assert.That(result.MeterReading.Account.Id, Is.EqualTo(2345));
            Assert.That(result.MeterReading.MeterReadingDateTime, Is.EqualTo(new DateTime(2019, 04, 22, 12, 25, 00)));
            Assert.That(result.MeterReading.Value, Is.EqualTo(45522));
        }

        [Test]
        public void Parsing_a_row_containing_a_missing_account_returns_an_error()
        {
            const string CsvRow = "9999,22/04/2019 12:25,45522";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                CsvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo("9999 is not a recognised Account ID."));            
        }
        
        [Test]
        public void Parsing_a_row_containing_an_invalid_account_returns_an_error()
        {
            const string CsvRow = "INVALID,22/04/2019 12:25,45522";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                CsvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo("INVALID is not a valid Account ID."));            
        }

        [Test]
        public void Parsing_a_row_containing_an_invalid_date_returns_an_error()
        {
            const string CsvRow = "2345,22/13/2019 12:25,45522";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                CsvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo("22/13/2019 12:25 is not a valid date/time."));            
        }

        [TestCase("-1")]
        [TestCase("100000")]
        public void Parsing_a_row_containing_an_out_of_range_meter_reading_value_returns_an_error(
            string meterReadingValue)
        {
            var csvRow = "2345,22/12/2019 12:25," + meterReadingValue;

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                csvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo(meterReadingValue + " is not within the valid meter reading value range of 0-99999."));            
        }

        [Test]
        public void Parsing_a_row_containing_an_invalid_meter_reading_value_returns_an_error()
        {
            var csvRow = "2345,22/12/2019 12:25,0X765";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                csvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo("0X765 is not a valid meter reading value."));            
        }

        [Test]
        public void Parsing_a_row_containing_an_insufficient_number_of_values_returns_an_error()
        {
            var csvRow = "blah,blah";

            var accounts = new[]
            {
                new Account(2345, "Fred", "Bloggs"),
            };

            var result = MeterReadingCsvRowParser.Parse(
                accounts,
                csvRow);

            Assert.That(result.MeterReading, Is.Null);
            Assert.That(result.Error, Is.EqualTo("blah,blah contains an insufficient number of values."));            
        }
    }
}