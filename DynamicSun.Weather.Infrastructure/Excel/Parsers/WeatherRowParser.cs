using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Domain.Entities;
using System.Globalization;
using NPOI.SS.UserModel;
using DynamicSun.Weather.Infrastructure.Excel.Constants;
using DynamicSun.Weather.Infrastructure.Excel.Mappings;

namespace DynamicSun.Weather.Infrastructure.Excel.Parsers
{
    public static class WeatherRowParser
    {
        public static DataResult<WeatherData> ParseWeatherRow(IRow row, int month, int year)
        {
            var errors = new List<IDataError>();
            var data = new WeatherData();

            // Дата и время
            var dateStr = ExcelCellValueHelper.GetCellStringValue(row.GetCell(WeatherRowParserConstants.DateCell));
            var timeStr = ExcelCellValueHelper.GetCellStringValue(row.GetCell(WeatherRowParserConstants.TimeCell));
            if (!DateTime.TryParseExact($"{dateStr} {timeStr}",
                    DateTimeFormatConstants.DDMMYYYYHHMM,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime dateTime))
            {
                errors.Add(new DataError($"Строка {row.RowNum + 1}: Неверная дата/время"));
            }
            else if (dateTime.Month != month || dateTime.Year != year)
            {
                errors.Add(new DataError($"Строка {row.RowNum + 1}: Дата не соответствует листу"));
            }
            else
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            }
            data.WeatherDateTime = dateTime;

            foreach (var (cellIndex, paramName) in WeatherFieldMappings.FieldNames)
            {
                if (WeatherFieldMappings.NumericFields.TryGetValue(cellIndex, out var setter))
                {
                    if (ExcelValueParser.TryParseDouble(row.GetCell(cellIndex), paramName, errors, row.RowNum, out double value, true))
                    {
                        setter(data, value);
                    }
                }
            }

            data.WindDirection = ExcelCellValueHelper.GetCellStringValue(row.GetCell(WeatherRowParserConstants.WindDirectionCell));
            data.Visibility = ExcelCellValueHelper.GetCellStringValue(row.GetCell(WeatherRowParserConstants.VisibilityCell));
            data.WeatherPhenomena = ExcelCellValueHelper.GetCellStringValue(row.GetCell(WeatherRowParserConstants.WeatherPhenomenaCell));

            return errors.Count == 0
                ? DataResult<WeatherData>.Success(data)
                : DataResult<WeatherData>.Failure(errors);
        }
    }
}
