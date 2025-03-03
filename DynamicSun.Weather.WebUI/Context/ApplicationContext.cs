using DynamicSun.Weather.Application.Mapping;
using DynamicSun.Weather.Application.Services;
using DynamicSun.Weather.Application.Services.Interfaces;
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
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Регистрация контроллеров
            services.AddControllers();

            // Регистрация Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(MappingProfile));


            // Подключение к PostgreSQL
            services.AddDbContext<WeatherDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("WeatherDbContext"));
            });

            // Регистрация Unit of Work
            services.AddScoped<IUnitOfWork, WeatherDbContext>();

            // Регистрация контекста базы данных
            //services.AddDbContext<LogisticsDbContext>(options =>
            //{
            //    options.UseNpgsql(configuration.GetConnectionString(nameof(LogisticsDbContext)));
            //});

            //services.AddAutoMapper(typeof(MappingProfile));

            // Регистрация и сервисов
            //services.AddScoped<IUnitOfWork, LogisticsDbContext>();
            //services.AddScoped<ILogisticsService, LogisticsService>();
            //services.AddScoped<IRfidTagService, RfidTagService>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IWeatherService, WeatherService>();
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
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
