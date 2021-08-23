namespace EnsekMeterReadings.Domain
{
    using System.Collections.Generic;

    public class MeterReadingsUploadResponse
    {
        public MeterReadingsUploadResponse(
            int numberOfSuccesses,
            IEnumerable<string> failures)
        {
            this.NumberOfSuccesses = numberOfSuccesses;
            this.Failures = failures;
        }

        public int NumberOfSuccesses { get; }

        public IEnumerable<string> Failures { get; }
    }
}
