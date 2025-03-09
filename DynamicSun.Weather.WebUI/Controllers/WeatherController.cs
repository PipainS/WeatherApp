using DynamicSun.Weather.Application.Constants;
using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSun.Weather.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController(
         IWeatherService weatherService,
         ILogger<WeatherController> logger) : ControllerBase
    {
        private readonly IWeatherService _weatherService = weatherService;
        private readonly ILogger<WeatherController> _logger = logger;

        [HttpGet("getAvailableDates")]
        public async Task<DataResponse<List<WeatherDateModel>>> GetAvailableDates()
        {
            var response = new DataResponse<List<WeatherDateModel>>();
            try
            {
                var data = await _weatherService.GetAvailableDates();

                if (data.IsSuccess)
                {
                    response = response with { Data = data.Data };
                    return response;
                }

                return response.AddError(new DataError(data.Errors[ErrorConstants.FirstErrorIndex].Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeatherController: GetAvailableDates: {Message}", ex.Message);
                return response.AddError(new DataError("Ошибка при получении доступных дат"));
            }
        }

        [HttpGet("getWeatherByDay/{year}/{month}/{day}")]
        public async Task<DataResponse<List<WeatherDataModel>>> GetWeatherByDay(int year, int month, int day)
        {
            var response = new DataResponse<List<WeatherDataModel>>();
            try
            {
                var data = await _weatherService.GetWeatherByDay(year, month, day);
                if (data.IsSuccess)
                {
                    response = response with { Data = data.Data };
                    return response;
                }
                return response.AddError(new DataError(data.Errors[ErrorConstants.FirstErrorIndex].Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeatherController: GetWeatherByDay: {Message}", ex.Message);
                return response.AddError(new DataError("Ошибка при получении данных о погоде на указанный день"));
            }
        }
    }
}
