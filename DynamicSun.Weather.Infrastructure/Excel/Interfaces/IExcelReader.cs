using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Domain.Entities;

namespace DynamicSun.Weather.Application.Services.Interfaces
{
    public interface IExcelReader
    {
        IDataResult<IEnumerable<WeatherData>> ReadWeatherData(Stream stream);
    }
}
