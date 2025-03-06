using DynamicSun.Weather.Application.Constants;
using DynamicSun.Weather.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace DynamicSun.Weather.Application.Validators
{
    public static class WeatherArchiveValidator
    {
        public static IDataResult<bool> ValidateFiles(List<IFormFile> files)
        {
            var errors = new List<IDataError>();

            if (files.Count > WeatherArchiveValidationConstants.MaxFilesPerRequest)
            {
                errors.Add(new DataError($"Максимум {WeatherArchiveValidationConstants.MaxFilesPerRequest} файлов за раз"));
            }

            long totalSize = 0;

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!WeatherArchiveValidationConstants.AllowedFileExtensions.Contains(extension))
                {
                    errors.Add(new DataError($"Недопустимый формат: {file.FileName}"));
                }

                if (file.Length > WeatherArchiveValidationConstants.MaxFileSizeInBytes)
                {
                    errors.Add(new DataError($"Файл {file.FileName} " +
                        $"превышает {WeatherArchiveValidationConstants.MaxFileSizeInBytes / 1024 / 1024 / 1024}GB"));
                }

                totalSize += file.Length;
            }

            if (totalSize > WeatherArchiveValidationConstants.MaxTotalSizeInBytes)
            {
                errors.Add(new DataError($"Общий размер " +
                    $"превышает {WeatherArchiveValidationConstants.MaxTotalSizeInBytes / 1024 / 1024 / 1024}GB"));
            }

            return errors.Count > 0
                ? DataResult<bool>.Failure(errors)
                : DataResult<bool>.Success(true);
        }
    }
}
