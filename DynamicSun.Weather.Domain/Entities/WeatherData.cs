using DynamicSun.Weather.Domain.Base;

namespace DynamicSun.Weather.Domain.Entities
{
    public class WeatherData : PersistentObject
    {
        // Combine date and time into one field for convenience
        public DateTime WeatherDateTime { get; set; }

        // Air temperature (°C)
        public double Temperature { get; set; }

        // Relative humidity (%)
        public double RelativeHumidity { get; set; }

        // Dew point (°C)
        public double DewPoint { get; set; }

        // Atmospheric pressure (mm Hg)
        public double AtmosphericPressure { get; set; }

        // Wind direction (e.g., "W, SW" or "calm")
        public string? WindDirection { get; set; }

        // Wind speed (m/s)
        public double? WindSpeed { get; set; }

        // Cloudiness (%)
        public double? Cloudiness { get; set; }

        // Cloud base height (h) – in meters
        public double CloudBaseHeight { get; set; }

        // Visibility (VV) – in meters; it may sometimes be absent, so the field is nullable
        public string? Visibility { get; set; }

        // Weather phenomena (e.g., "Haze")
        public string? WeatherPhenomena { get; set; }
    }
}
