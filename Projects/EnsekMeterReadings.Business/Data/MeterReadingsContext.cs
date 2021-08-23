namespace EnsekMeterReadings.Business.Data
{
    using EnsekMeterReadings.Business.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class MeterReadingsContext : DbContext
    {
        public DbSet<DbAccount> Accounts { get; set; }

        public DbSet<DbMeterReading> MeterReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Put this in env variable or in startup
            optionsBuilder.UseSqlServer(@"Server=(local);Database=EnsekMeterReadings;Trusted_Connection=True;");
        }
    }
}
