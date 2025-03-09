namespace DynamicSun.Weather.Application.Constants
{
    public static class WeatherArchiveValidationConstants
    {
        public const int MaxFilesPerRequest = 10;
        public const long MaxFileSizeInBytes = 1L * 1024 * 1024 * 1024; // 1GB
        public const long MaxTotalSizeInBytes = 5L * 1024 * 1024 * 1024; // 5GB
        public static readonly string[] AllowedFileExtensions = { ".xls", ".xlsx" };
    }
}
