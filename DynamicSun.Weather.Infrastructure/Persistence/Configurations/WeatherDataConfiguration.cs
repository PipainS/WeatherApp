using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DynamicSun.Weather.Domain.Entities;

namespace DynamicSun.Weather.Infrastructure.Persistence.Configurations
{
    public class WeatherDataConfiguration : IEntityTypeConfiguration<WeatherData>
    {
        public void Configure(EntityTypeBuilder<WeatherData> builder)
        {
            builder.ToTable("WeatherData"); // Название таблицы в БД

            builder.HasKey(w => w.Id); // Первичный ключ

            builder.Property(w => w.WeatherDateTime)
                   .IsRequired();

            builder.Property(w => w.Temperature)
                   .HasColumnType("decimal(5,2)") // Два знака после запятой
                   .IsRequired();

            builder.Property(w => w.RelativeHumidity)
                   .IsRequired();

            builder.Property(w => w.DewPoint)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(w => w.AtmosphericPressure)
                   .IsRequired();

            builder.Property(w => w.WindDirection)
                   .HasMaxLength(10) // Ограничим длину строки
                   .IsRequired();

            builder.Property(w => w.WindSpeed)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(w => w.Cloudiness)
                   .IsRequired();

            builder.Property(w => w.CloudBaseHeight)
                   .IsRequired();

            builder.Property(w => w.Visibility)
                   .IsRequired(false); // Может быть null

            builder.Property(w => w.WeatherPhenomena)
                   .HasMaxLength(50); // Достаточно для описания погоды
        }
    }
}
