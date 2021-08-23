namespace EnsekMeterReadings.WebApi.Controllers
{
    using EnsekMeterReadings.Domain;
    using EnsekMeterReadings.Domain.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    [ApiController]
    [Route("meter-reading-uploads")]
    public class MeterReadingsUploadController : ControllerBase
    {
        private readonly IUploadMeterReadingsService uploadMeterReadingsService;

        public MeterReadingsUploadController(IUploadMeterReadingsService uploadMeterReadingsService)
        {
            this.uploadMeterReadingsService = uploadMeterReadingsService;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult> UploadMeterReadings()
        {
            MeterReadingsUploadResponse response;

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var meterReadings = await reader.ReadToEndAsync();

                response = await this.uploadMeterReadingsService.UploadAsync(meterReadings);
            }

            return this.Ok(JsonSerializer.Serialize(response));
        }
    }
}
