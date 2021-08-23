namespace EnsekMeterReadings.WebApi
{
    using EnsekMeterReadings.Business.Data;
    using EnsekMeterReadings.Business.Services;
    using EnsekMeterReadings.Domain.Abstractions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MeterReadingsContext>();
            services.AddScoped<IMeterReadingsRepository, MeterReadingsRepository>();
            services.AddScoped<IUploadMeterReadingsService, UploadMeterReadingsService>();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
