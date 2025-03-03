using AutoMapper;
using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Domain.Entities;
using DynamicSun.Weather.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DynamicSun.Weather.Application.Services
{
    public class WeatherService(
        IUnitOfWork uow,
        IMapper mapper,
        ILogger<WeatherService> logger) : IWeatherService
    {
        private readonly IUnitOfWork _uow = uow;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<WeatherService> _logger = logger;

        public async Task<IDataResult<List<WeatherDataModel>>> GetAll()
        {
            try
            {
                var entities = await _uow.GetAll<WeatherData>()
                    .AsNoTracking()
                    .ToListAsync();

                if (entities == null || entities.Count == 0)
                {
                    return DataResult<List<WeatherDataModel>>.Failure(
                        new DataError("Weather data not found")
                    );
                }

                var result = _mapper.Map<List<WeatherDataModel>>(entities);

                if (result == null || result.Count == 0)
                {
                    return DataResult<List<WeatherDataModel>>.Failure(
                        new DataError("Error processing weather data")
                    );
                }

                return DataResult<List<WeatherDataModel>>.Success(result);
            }
            catch (Exception ex)
            {
                // улучшить логи
                _logger.LogError(ex, "WeatherService Database error occurred while fetching weather data");

                return DataResult<List<WeatherDataModel>>.Failure(
                    new DataError("Internal server error"));
            }
        }
    }
}