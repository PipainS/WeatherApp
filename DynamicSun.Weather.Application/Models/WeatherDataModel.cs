namespace DynamicSun.Weather.Application.Models
{
    public class WeatherDataModel
    {
        public WeatherDataModel() { } // for mapper

        // Combine date and time into one field for convenience
        public DateTime WeatherDateTime { get; set; }

        // Air temperature (°C)
        public double Temperature { get; set; }

        // Relative air humidity (%)
        public double RelativeHumidity { get; set; }

        // Dew point (°C)
        public double DewPoint { get; set; }

        // Atmospheric pressure (mm Hg)
        public double AtmosphericPressure { get; set; }

        // Wind direction (e.g., "W,SW" or "calm")
        public string WindDirection { get; set; }

        // Wind speed (m/s)
        public double WindSpeed { get; set; }

        // Cloudiness (%)
        public double Cloudiness { get; set; }

        // Cloud base height (m) – in meters
        public double CloudBaseHeight { get; set; }

        // Visibility (VV) – in meters; may be null, so making the field nullable
        public string? Visibility { get; set; }

        // Weather phenomena (e.g., "Haze")
        public string WeatherPhenomena { get; set; }
    }
}
