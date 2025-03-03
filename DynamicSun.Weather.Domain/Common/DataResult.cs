using System.Collections.ObjectModel;

namespace DynamicSun.Weather.Domain.Common
{
    // Реализация результата с использованием record для иммутабельности
    public record DataResult<T> : IDataResult<T>
    {
        public T? Data { get; init; }
        public IReadOnlyList<IDataError> Errors { get; init; } = Array.Empty<IDataError>();
        public bool IsSuccess => !Errors.Any();

        private DataResult() { }

        public static DataResult<T> Success(T data) => new() { Data = data };

        public static DataResult<T> Failure(params IDataError[] errors) => new()
        {
            Errors = new ReadOnlyCollection<IDataError>(errors)
        };

        public static DataResult<T> Failure(IEnumerable<IDataError> errors) => new()
        {
            Errors = new ReadOnlyCollection<IDataError>(errors.ToArray())
        };
    }
}
