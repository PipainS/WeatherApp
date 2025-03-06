using DynamicSun.Weather.Domain.Entities;
using DynamicSun.Weather.Infrastructure.Excel.Constants;

namespace DynamicSun.Weather.Infrastructure.Excel.Mappings
{
    public static class WeatherFieldMappings
    {
        public static readonly Dictionary<int, Action<WeatherData, double>> NumericFields = new()
        {
            { WeatherRowParserConstants.TemperatureCell, (data, value) => data.Temperature = value },
            { WeatherRowParserConstants.HumidityCell, (data, value) => data.RelativeHumidity = value },
            { WeatherRowParserConstants.DewPointCell, (data, value) => data.DewPoint = value },
            { WeatherRowParserConstants.PressureCell, (data, value) => data.AtmosphericPressure = value },
            { WeatherRowParserConstants.WindSpeedCell, (data, value) => data.WindSpeed = value },
            { WeatherRowParserConstants.CloudinessCell, (data, value) => data.Cloudiness = value },
            { WeatherRowParserConstants.CloudBaseHeightCell, (data, value) => data.CloudBaseHeight = value }
        };

        public static readonly Dictionary<int, string> FieldNames = new()
        {
            { WeatherRowParserConstants.TemperatureCell, WeatherParameterNames.Temperature },
            { WeatherRowParserConstants.HumidityCell, WeatherParameterNames.Humidity },
            { WeatherRowParserConstants.DewPointCell, WeatherParameterNames.DewPoint },
            { WeatherRowParserConstants.PressureCell, WeatherParameterNames.Pressure },
            { WeatherRowParserConstants.WindSpeedCell, WeatherParameterNames.WindSpeed },
            { WeatherRowParserConstants.CloudinessCell, WeatherParameterNames.Cloudiness },
            { WeatherRowParserConstants.CloudBaseHeightCell, WeatherParameterNames.CloudBaseHeight }
        };
    }
}
