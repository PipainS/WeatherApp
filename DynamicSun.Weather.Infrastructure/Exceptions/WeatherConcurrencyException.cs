namespace DynamicSun.Weather.Infrastructure.Exceptions
{
    public class WeatherConcurrencyException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
