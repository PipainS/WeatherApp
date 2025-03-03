using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSun.Weather.Domain.Common
{
    public sealed record DataResponse<T>
    {
        public T? Data { get; init; }
        public IReadOnlyList<IDataError> Errors { get; init; } = [];
        public bool IsValid => Errors.Count == 0;

        // Для удобного создания через fluent-API
        public DataResponse<T> AddError(IDataError error)
            => this with { Errors = [.. Errors, error] };
    }
}
