using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DynamicSun.Weather.Domain.Entities;

namespace DynamicSun.Weather.Infrastructure.Persistence.Configurations
{
    public class WeatherDataConfiguration : IEntityTypeConfiguration<WeatherData>
    {
        public void Configure(EntityTypeBuilder<WeatherData> builder)
        {
            builder.ToTable("WeatherData");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.WeatherDateTime)
                   .HasColumnType("timestamp without time zone")
                   .IsRequired();

            builder.Property(w => w.Temperature)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired();

            builder.Property(w => w.RelativeHumidity)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(w => w.DewPoint)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired();

            builder.Property(w => w.AtmosphericPressure)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired();

            builder.Property(w => w.WindDirection)
                   .HasMaxLength(10);

            builder.Property(w => w.WindSpeed)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired(false);

            builder.Property(w => w.Cloudiness)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired(false);

            builder.Property(w => w.CloudBaseHeight)
                   .HasColumnType("decimal(7,2)")
                   .IsRequired();

            builder.Property(w => w.Visibility);

            builder.Property(w => w.WeatherPhenomena);
        }
    }
}
