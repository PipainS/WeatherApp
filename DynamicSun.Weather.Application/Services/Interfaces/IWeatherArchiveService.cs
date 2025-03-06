using DynamicSun.Weather.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace DynamicSun.Weather.Application.Services.Interfaces
{
    public interface IWeatherArchiveService
    {
        Task<IDataResult<bool>> UploadWeatherArchivesAsync(List<IFormFile> archives);
    }
}
