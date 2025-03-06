using DynamicSun.Weather.Application.Constants;
using DynamicSun.Weather.Application.Mapping;
using DynamicSun.Weather.Application.Services;
using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Infrastructure.Excel;
using DynamicSun.Weather.Infrastructure.Persistence;
using DynamicSun.Weather.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicSun.Weather.WebUI.Context
{

    public static class ApplicationContext
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AppConstants.CorsPolicyName,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<WeatherDbContext>(options =>  
            {
                options.UseNpgsql(configuration.GetConnectionString(AppConstants.WeatherDbContext));
            });

            services.AddScoped<IUnitOfWork, WeatherDbContext>();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IWeatherArchiveService, WeatherArchiveService>();
            services.AddScoped<IExcelReader, ExcelReader>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(AppConstants.CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
