using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Weather.Domain.Common
{
    // Базовый класс ошибки
    public interface IDataError
    {
        string Message { get; }
    }
}
