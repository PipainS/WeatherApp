namespace DynamicSun.Weather.Infrastructure.Exceptions
{
    public class WeatherDataSavingException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
