namespace EnsekMeterReadings.Business.Data
{
    using EnsekMeterReadings.Business.Data.Entities;
    using EnsekMeterReadings.Domain;
    using EnsekMeterReadings.Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MeterReadingsRepository : IMeterReadingsRepository
    {
        private readonly MeterReadingsContext meterReadingsContext;

        public MeterReadingsRepository(MeterReadingsContext meterReadingsContext)
        {
            this.meterReadingsContext = meterReadingsContext;
        }

        public async Task<IReadOnlyCollection<Account>> GetAllAccountsAsync()
        {
            return await this.meterReadingsContext.Accounts
                .Select(acc =>
                    new Account(acc.Id, acc.FirstName, acc.LastName))
                .ToArrayAsync();
        }

        public async Task AddMeterReadingAsync(MeterReading meterReading)
        {
            var dbMeterReading = new DbMeterReading
            {
                AccountId = meterReading.Account.Id,
                MeterReadingDateTime = meterReading.MeterReadingDateTime,
                Value = meterReading.Value,
            };

            meterReadingsContext.Add(dbMeterReading);

            await meterReadingsContext.SaveChangesAsync();
        }
    }
}
