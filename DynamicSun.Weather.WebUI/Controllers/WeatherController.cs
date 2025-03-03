using DynamicSun.Weather.Application.Models;
using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet]
        public async Task<DataResponse<List<WeatherDataModel>>> GetAllWeatherData()
        {
            var response = new DataResponse<List<WeatherDataModel>>();
            try
            {
                var data = await _weatherService.GetAll();

                if (data.IsSuccess)
                {
                    return response;
                }
                return response.AddError(new DataError(data.Errors[0].Message));

            }
            catch (Exception ex)
            {
                // улучшить логи и отправку ошибки
                _logger.LogError(ex, "Unhandled exception in WeatherController");
                return response.AddError(new DataError("Error"));
            }
        }
    }
}
