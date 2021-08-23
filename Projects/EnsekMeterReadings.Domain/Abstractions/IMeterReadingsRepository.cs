namespace EnsekMeterReadings.Domain.Abstractions
{
    using EnsekMeterReadings.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMeterReadingsRepository
    {
        Task<IReadOnlyCollection<Account>> GetAllAccountsAsync();

        Task AddMeterReadingAsync(MeterReading meterReading);
    }
}