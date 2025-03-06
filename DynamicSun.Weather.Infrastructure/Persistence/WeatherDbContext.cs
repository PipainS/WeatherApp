using Microsoft.EntityFrameworkCore;
using DynamicSun.Weather.Infrastructure.Persistence.Configurations;
using DynamicSun.Weather.Application.Utils;

namespace DynamicSun.Weather.Infrastructure.Persistence
{
    public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : AppDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentChecker.NotNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new WeatherDataConfiguration());
        }
    }
}
