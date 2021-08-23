namespace EnsekMeterReadings.Domain.Abstractions
{
    using System.Threading.Tasks;

    public interface IUploadMeterReadingsService
    {
        Task<MeterReadingsUploadResponse> UploadAsync(string meterReadings);
    }
}