using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DynamicSun.Weather.Domain;
using DynamicSun.Weather.Infrastructure.Persistence.Interfaces;
using DynamicSun.Weather.Domain.Base;

namespace DynamicSun.Weather.Infrastructure.Persistence
{
    public abstract class AppDbContext : DbContext, IUnitOfWork
    {
        protected AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public IQueryable<T> GetAll<T>() where T : PersistentObject
        {
            return Set<T>().AsQueryable();
        }

        public async Task<T> GetAsync<T>(int entityId) where T : PersistentObject
        {
            return await Set<T>().FindAsync(entityId);
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
                // Логируем и пробрасываем
                throw new Exception("Ошибка конкурентного обновления", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ошибка обновления базы данных", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при сохранении данных", ex);
            }
        }

    }
}

