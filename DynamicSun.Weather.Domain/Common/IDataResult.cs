namespace DynamicSun.Weather.Domain.Common
{
    public interface IDataResult
    {
        IReadOnlyList<IDataError> Errors { get; }
        bool IsSuccess { get; }
    }

    public interface IDataResult<T> : IDataResult
    {
        T? Data { get; }
    }
}
