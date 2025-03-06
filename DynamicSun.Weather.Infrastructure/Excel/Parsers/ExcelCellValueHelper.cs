using System.Globalization;
using NPOI.SS.UserModel;

namespace DynamicSun.Weather.Infrastructure.Excel.Parsers
{
    public static class ExcelCellValueHelper
    {
        public static string GetCellStringValue(ICell? cell)
        {
            if (cell == null) return string.Empty;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        double oaDate = Convert.ToDouble(cell.DateCellValue);
                        DateTime date = DateTime.FromOADate(oaDate);
                        return date.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

                    }
                    else
                    {
                        return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                    }

                case CellType.String:
                    return cell.StringCellValue.Trim();

                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();

                case CellType.Formula:
                    return cell.CachedFormulaResultType switch
                    {
                        CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
                        CellType.String => cell.StringCellValue.Trim(),
                        CellType.Boolean => cell.BooleanCellValue.ToString(),
                        _ => string.Empty,
                    };
                default:
                    return string.Empty;
            }
        }
    }
}
