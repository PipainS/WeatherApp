using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Domain.Common;

namespace DynamicSun.Weather.Application.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<IDataResult<List<WeatherDataModel>>> GetAll();
    }
}
