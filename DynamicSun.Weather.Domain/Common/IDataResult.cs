using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Weather.Domain.Common
{
    // Необобщенный интерфейс для базового результата
    public interface IDataResult
    {
        IReadOnlyList<IDataError> Errors { get; }
        bool IsSuccess { get; }
    }

    // Обобщенный интерфейс с данными
    public interface IDataResult<T> : IDataResult
    {
        T? Data { get; }
    }
}
