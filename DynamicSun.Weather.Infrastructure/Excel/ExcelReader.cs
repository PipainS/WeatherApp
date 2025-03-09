using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Domain.Entities;
using DynamicSun.Weather.Infrastructure.Excel.Parsers;
using Microsoft.Extensions.Logging;
using DynamicSun.Weather.Infrastructure.Excel.Constants;
using DynamicSun.Weather.Infrastructure.Excel.Interfaces;

namespace DynamicSun.Weather.Infrastructure.Excel
{
    public class ExcelReader(ILogger<ExcelReader> logger) : IExcelReader
    {
        private readonly ILogger<ExcelReader> _logger = logger;

        public IDataResult<IEnumerable<WeatherData>> ReadWeatherData(Stream stream)
        {
            if (stream == null)
            {
                _logger.LogError("Ошибка в {ClassName}.{MethodName}: Параметр потока равен null",
                    nameof(ExcelReader), nameof(ReadWeatherData));
                throw new ArgumentNullException(nameof(stream), "Поток не может быть null");
            }

            var errors = new List<IDataError>();
            var weatherDataList = new List<WeatherData>();

            try
            {
                using IWorkbook workbook = CreateWorkbook(stream);

                for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                {
                    var sheet = workbook.GetSheetAt(sheetIndex);
                    if (sheet == null) continue;

                    var (month, year, sheetError) = ExcelHeaderParser.ParseSheetHeader(sheet);

                    if (sheetError != null)
                    {
                        errors.Add(sheetError);
                        continue;
                    }

                    for (int rowIndex = ExcelConstants.DataStartRow; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        var row = sheet.GetRow(rowIndex);
                        if (row == null) continue;

                        var result = WeatherRowParser.ParseWeatherRow(row, month, year);
                        if (result.IsSuccess)
                            weatherDataList.Add(result.Data!);
                        else
                            _logger.LogWarning("Ошибка парсинга строки {RowIndex} на листе {SheetName}", rowIndex, sheet.SheetName);
                            errors.AddRange(result.Errors);
                    }
                }

                return errors.Count != 0
                     ? DataResult<IEnumerable<WeatherData>>.Failure(errors)
                     : DataResult<IEnumerable<WeatherData>>.Success(weatherDataList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в {ClassName}.{MethodName}: {ErrorMessage}",
                    nameof(ExcelReader), nameof(ReadWeatherData), ex.Message);

                errors.Add(new DataError($"Ошибка чтения файла"));
                return DataResult<IEnumerable<WeatherData>>.Failure(errors);
            }
        }

        private static IWorkbook CreateWorkbook(Stream stream)
        {
            try
            {
                return new XSSFWorkbook(stream);
            }
            catch
            {
                stream.Position = 0;
                return new HSSFWorkbook(stream);
            }
        }
    }
}