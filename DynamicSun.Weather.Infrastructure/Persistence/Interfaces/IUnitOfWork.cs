using DynamicSun.Weather.Domain.Base;

namespace DynamicSun.Weather.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : PersistentObject;
        void AddRangeOnSubmit<T>(IEnumerable<T> entities) where T : PersistentObject;
        Task SubmitAsync();
    }
}
