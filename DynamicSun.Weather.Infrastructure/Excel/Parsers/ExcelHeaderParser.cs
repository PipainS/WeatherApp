using System.Text.RegularExpressions;
using DynamicSun.Weather.Domain.Common;
using DynamicSun.Weather.Infrastructure.Excel.Constants;
using NPOI.SS.UserModel;

namespace DynamicSun.Weather.Infrastructure.Excel.Parsers
{
    public static class ExcelHeaderParser
    {
        public static (int month, int year, IDataError? error) ParseSheetHeader(ISheet? sheet)
        {
            if (sheet == null)
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear, 
                    new DataError("Лист не найден"));

            var headerRow = sheet.GetRow(ExcelHeaderParserConstants.HeaderRowIndex);
            if (headerRow == null || headerRow.LastCellNum == 0)
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear, 
                    new DataError("Отсутствует заголовок листа"));

            var headerCell = headerRow.GetCell(ExcelHeaderParserConstants.HeaderCellIndex)?.StringCellValue?.Trim();
            if (string.IsNullOrEmpty(headerCell))
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear,
                    new DataError("Заголовок пуст или отсутствует"));

            var match = Regex.Match(headerCell, RegexPatterns.HeaderPattern, RegexOptions.IgnoreCase);
            if (!match.Success)
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear, 
                    new DataError($"Неверный формат заголовка: {headerCell}"));

            var monthName = match.Groups[1].Value.ToLower();
            if (!MonthHelper.TryGetMonthNumber(monthName, out int month))
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear,
                    new DataError($"Неизвестный месяц: {monthName}"));

            if (!int.TryParse(match.Groups[2].Value, out int year))
                return (ExcelHeaderParserConstants.DefaultMonth, ExcelHeaderParserConstants.DefaultYear,
                    new DataError($"Неверный год: {match.Groups[2].Value}"));

            return (month, year, null);
        }
    }
}
