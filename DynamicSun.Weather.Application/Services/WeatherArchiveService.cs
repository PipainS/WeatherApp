using DynamicSun.Weather.Application.Services.Interfaces;
using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DynamicSun.Weather.Application.Validators;
using DynamicSun.Weather.Application.Constants;

namespace DynamicSun.Weather.Application.Services
{
    public class WeatherArchiveService : IWeatherArchiveService
    {
        private readonly IExcelReader _excelReader;
        private readonly ILogger<WeatherArchiveService> _logger;
        private readonly IUnitOfWork _uow;

        public WeatherArchiveService(
            IExcelReader excelReader,
            ILogger<WeatherArchiveService> logger,
            IUnitOfWork uow)
        {
            _excelReader = excelReader;
            _logger = logger;
            _uow = uow;
        }

        public async Task<IDataResult<bool>> UploadWeatherArchivesAsync(List<IFormFile> archives)
        {
            var validationResult = WeatherArchiveValidator.ValidateFiles(archives);
            if (!validationResult.IsSuccess)
                return validationResult;

            var errors = new List<IDataError>();

            try
            {
                var processingTasks = archives.Select(file => ProcessFileAsync(file, errors));
                await Task.WhenAll(processingTasks);

                await _uow.SubmitAsync();
                return errors.Count == 0
                    ? DataResult<bool>.Success(true)
                    : DataResult<bool>.Failure(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Class}.{Method}: Критическая ошибка загрузки архивов",
                    nameof(WeatherArchiveService), nameof(UploadWeatherArchivesAsync));
                return DataResult<bool>.Failure(new DataError("Произошла ошибка при обработке файлов. Попробуйте снова."));
            }
        }

        private async Task ProcessFileAsync(IFormFile file, List<IDataError> errors)
        {
            try
            {
                await using var stream = file.OpenReadStream();
                var readResult = _excelReader.ReadWeatherData(stream);
                if (!readResult.IsSuccess)
                {
                    errors.Add(new DataError($"Файл {file.FileName} не может быть обработан. " +
                        $"{readResult.Errors[ErrorConstants.FirstErrorIndex].Message}"));
                    return;
                }

                if (readResult.Data is null)
                {
                    errors.Add(new DataError($"Файл {file.FileName} содержит некорректные данные."));
                    return;
                }

                _uow.AddRangeOnSubmit(readResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Class}.{Method}: Ошибка при обработке файла {FileName}",
                    nameof(WeatherArchiveService), nameof(ProcessFileAsync), file.FileName);
                errors.Add(new DataError($"Файл {file.FileName} не может быть обработан."));
            }
        }
    }
}