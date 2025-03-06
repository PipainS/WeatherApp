using System.Globalization;
using AutoMapper;
using DynamicSun.Weather.Application.Constants;
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

        public async Task<IDataResult<List<WeatherDateModel>>> GetAvailableDates()
        {
            try
            {
                var dbDates = await _uow.GetAll<WeatherData>()
                    .GroupBy(w => new { w.WeatherDateTime.Year, w.WeatherDateTime.Month })
                    .Select(g => new { g.Key.Year, g.Key.Month })
                    .OrderByDescending(x => x.Year)
                    .ThenByDescending(x => x.Month)
                    .ToListAsync();

                if (dbDates.Count == 0)
                {
                    return DataResult<List<WeatherDateModel>>.Failure(
                        new DataError("Доступных дат не найдено"));
                }

                var russianCulture = new CultureInfo(CultureConstants.RussianCulture);
                var result = dbDates
                    .GroupBy(x => x.Year)
                    .Select(g => new WeatherDateModel
                    {
                        Year = g.Key,
                        Months = g.Select(x =>
                            DateTimeFormatInfo.GetInstance(russianCulture)
                                .GetMonthName(x.Month))
                            .Distinct()
                            .ToList()
                    })
                    .ToList();

                return DataResult<List<WeatherDateModel>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeatherService: GetAvailableDates. Database error occurred while fetching available weather dates");
                return DataResult<List<WeatherDateModel>>.Failure(
                    new DataError("Ошибка на стороне сервера"));
            }
        }

        public async Task<IDataResult<List<WeatherDataModel>>> GetWeatherByDay(int year, int month, int day)
        {
            try
            {
                var weatherDataList = await _uow.GetAll<WeatherData>()
                    .AsNoTracking()
                    .Where(w => w.WeatherDateTime.Year == year &&
                                w.WeatherDateTime.Month == month &&
                                w.WeatherDateTime.Day == day)
                    .ToListAsync();

                if (weatherDataList.Count == 0)
                {
                    return DataResult<List<WeatherDataModel>>.Failure(new DataError("Записи о погоде за выбранный день отсутствуют"));
                }

                var result = _mapper.Map<List<WeatherDataModel>>(weatherDataList);

                return DataResult<List<WeatherDataModel>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeatherService: GetAvailableDates" +
                    "Error occurred while fetching weather data for {Year}-{Month}-{Day}.", year, month, day);
                return DataResult<List<WeatherDataModel>>.Failure(new DataError("Ошибка на стороне сервера"));
            }
        }
    }
}