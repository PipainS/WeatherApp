using DynamicSun.Weather.Infrastructure.Excel.enums;

namespace DynamicSun.Weather.Infrastructure.Excel.Parsers
{
    public static class MonthHelper
    {
        private static readonly Dictionary<string, Month> MonthMap = new()
        {
            { "январь", Month.January }, { "февраль", Month.February }, { "март", Month.March },
            { "апрель", Month.April }, { "май", Month.May }, { "июнь", Month.June },
            { "июль", Month.July }, { "август", Month.August }, { "сентябрь", Month.September },
            { "октябрь", Month.October }, { "ноябрь", Month.November }, { "декабрь", Month.December }
        };

        public static bool TryGetMonthNumber(string monthName, out int month)
        {
            if (MonthMap.TryGetValue(monthName.ToLower(), out Month parsedMonth))
            {
                month = (int)parsedMonth;
                return true;
            }

            month = 0;
            return false;
        }
    }
}
