using Microsoft.EntityFrameworkCore;
using DynamicSun.Weather.Domain.Entities;
using DynamicSun.Weather.Infrastructure.Persistence.Configurations;

namespace DynamicSun.Weather.Infrastructure.Persistence
{
    public class WeatherDbContext : AppDbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherData> WeatherDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ArgumentChecker.NotNull(modelBuilder, nameof(modelBuilder)); // Проверяем, что modelBuilder не null

            modelBuilder.HasDefaultSchema("public"); // Устанавливаем схему для PostgreSQL

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WeatherDataConfiguration()); // Добавляем нашу конфигурацию
            // Применяем конфигурации сущностей
            //modelBuilder.ApplyConfiguration(new WeatherConfiguration());
            //modelBuilder.ApplyConfiguration(new CityConfiguration());
        }

    }
}
