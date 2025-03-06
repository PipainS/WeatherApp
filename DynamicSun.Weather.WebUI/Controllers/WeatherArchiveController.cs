using DynamicSun.Weather.Application.Constants;
using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSun.Weather.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherArchiveController(
        IWeatherArchiveService weatherArchiveService,
        ILogger<WeatherArchiveController> logger) : ControllerBase  
    {
        private readonly IWeatherArchiveService _weatherArchiveService = weatherArchiveService;
        private readonly ILogger<WeatherArchiveController> _logger = logger;


        [HttpPost("uploadWeatherArchives")]
        public async Task<DataResponse<bool>> UploadWeatherArchives(List<IFormFile> archives)
        {
            var response = new DataResponse<bool>();
            try
            {
                var data = await _weatherArchiveService.UploadWeatherArchivesAsync(archives);

                if (data.IsSuccess)
                {
                    return response with { Data = data.Data };
                }
                return response.AddError(new DataError(data.Errors[ErrorConstants.FirstErrorIndex].Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "WeatherArchiveController: UploadWeatherArchives: {Message}", ex.Message);
                return response.AddError(new DataError("Ошибка при загрузке архива"));
            }
        }
    }
}
