﻿using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DynamicSun.Weather.Domain.Base;

namespace DynamicSun.Weather.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : PersistentObject;
        Task<T> GetAsync<T>(int entityId) where T : PersistentObject;
        void AddRangeOnSubmit<T>(IEnumerable<T> entities) where T : PersistentObject;
        Task SubmitAsync();
    }
}
