using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GasAwareness.API.Entities;

namespace GasAwareness.API.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class, IEntityBase
    {
        Task<T?> GetEntityAsync(string[]? includes = null, Expression<Func<T, bool>>? filter = null);
        Task<List<T>> GetEntityListAsync(string[]? includes = null, Expression<Func<T, bool>>? filter = null);
        Task<T?> CreateEntityAsync(T entity);
        Task<List<T>> CreateEntityAsync(List<T> entity);
        Task<T?> UpdateEntityAsync(T entity);
        Task<bool> DeleteEntityAsync(Guid id);
    }
}