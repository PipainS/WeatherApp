using System.Globalization;
using DynamicSun.Weather.Domain.Common;
using NPOI.SS.UserModel;

namespace DynamicSun.Weather.Infrastructure.Excel.Parsers
{
    public static class ExcelValueParser
    {
        public static bool TryParseDouble(ICell? cell, string fieldName, List<IDataError> errors, int rowNum, out double value, bool isOptional = false)
        {
            return TryParseValue(cell, fieldName, errors, rowNum, out value,
                (string s, out double result) => double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result), isOptional);
        }

        public static bool TryParseInt(ICell? cell, string fieldName, List<IDataError> errors, int rowNum, out int value, bool isOptional = false)
        {
            return TryParseValue(cell, fieldName, errors, rowNum, out value,
                (string s, out int result) => int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result), isOptional);
        }

        private static bool TryParseValue<T>(ICell? cell, string fieldName, List<IDataError> errors, int rowNum, out T value,
                                             TryParseDelegate<T> tryParseFunc, bool isOptional)
        {
            if (rowNum == 47)
            {
                Console.WriteLine("");
            }
            value = default!;
            var str = ExcelCellValueHelper.GetCellStringValue(cell)?.Replace(',', '.') ?? string.Empty;

            if (string.IsNullOrWhiteSpace(str))
            {
                if (isOptional) return false;
                errors.Add(new DataError($"Строка {rowNum + 1}: Поле '{fieldName}' обязательно для заполнения"));
                return false;
            }

            if (!tryParseFunc(str, out value))
            {
                errors.Add(new DataError($"Строка {rowNum + 1}: Ошибка в поле '{fieldName}'"));
                return false;
            }
            return true;
        }

        private delegate bool TryParseDelegate<T>(string input, out T value);
    }
}
