using Microsoft.EntityFrameworkCore;
using DynamicSun.Weather.Infrastructure.Persistence.Interfaces;
using DynamicSun.Weather.Domain.Base;
using DynamicSun.Weather.Infrastructure.Exceptions;

namespace DynamicSun.Weather.Infrastructure.Persistence
{
    public abstract class AppDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        public IQueryable<T> GetAll<T>() where T : PersistentObject
        {
            return Set<T>().AsQueryable();
        }

        public void AddRangeOnSubmit<T>(IEnumerable<T> entities) where T : PersistentObject
        {
            Set<T>().AddRange(entities);
        }

        public async Task SubmitAsync()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new WeatherConcurrencyException("Ошибка конкурентного обновления", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new WeatherDbUpdateException("Ошибка обновления базы данных", ex);
            }
            catch (Exception ex)
            {
                throw new WeatherDataSavingException("Ошибка при сохранении данных", ex);
            }
        }
    }
}

