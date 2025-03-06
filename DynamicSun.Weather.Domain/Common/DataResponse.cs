namespace DynamicSun.Weather.Domain.Common
{
    public sealed record DataResponse<T>
    {
        public T? Data { get; init; }

        public IReadOnlyList<IDataError> Errors { get; init; } = [];

        public bool IsValid => Errors.Count == 0;

        public DataResponse<T> AddError(IDataError error)
            => this with { Errors = [.. Errors, error] };
    }
}
