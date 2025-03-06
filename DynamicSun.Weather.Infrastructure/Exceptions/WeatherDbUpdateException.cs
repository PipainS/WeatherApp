namespace DynamicSun.Weather.Infrastructure.Exceptions
{
    public class WeatherDbUpdateException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
