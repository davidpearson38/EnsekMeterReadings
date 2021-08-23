namespace EnsekMeterReadings.Domain
{
    using System;

    public class MeterReading
    {
        public MeterReading(Account account, DateTime meterReadingDateTime, int value)
        {
            this.Account = account;
            this.MeterReadingDateTime = meterReadingDateTime;
            this.Value = value;
        }

        public Account Account { get; }

        public DateTime MeterReadingDateTime { get; }

        public int Value { get; }
    }
}
