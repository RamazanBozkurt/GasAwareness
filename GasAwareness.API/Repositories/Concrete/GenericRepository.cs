using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GasAwareness.API.Entities;
using GasAwareness.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GasAwareness.API.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntityBase
    {
        public readonly DataContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T?> CreateEntityAsync(T entity)
        {
            _db.Add(entity);
            bool response = Convert.ToBoolean(await _context.SaveChangesAsync());

            if (!response) return null;

            return entity;
        }

        public async Task<List<T>> CreateEntityAsync(List<T> entity)
        {
            await _db.AddRangeAsync(entity);
            
            bool response = Convert.ToBoolean(await _context.SaveChangesAsync());

            if (!response) return new List<T>();

            return entity;
        }

        public async Task<bool> DeleteEntityAsync(Guid id)
        {
            var entity = await _db.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            var response = await UpdateEntityAsync(entity);

            if (response == null) return false;

            return true;
        }

        public async Task<T?> GetEntityAsync(string[]? includes = null, System.Linq.Expressions.Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> response = _db;

            if (filter != null) response = response.Where(filter);

            if (includes != null && includes.Any())
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    if (string.IsNullOrEmpty(includes[i])) continue;

                    response = response.Include(includes[i]);
                }
            }

            return await response.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetEntityListAsync(string[]? includes = null, System.Linq.Expressions.Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> response = _db;

            if (filter != null) response = response.Where(filter);

            if (includes != null && includes.Any())
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    if (string.IsNullOrEmpty(includes[i])) continue;

                    response = response.Include(includes[i]);
                }
            }

            return await response.ToListAsync();
        }

        public async Task<T?> UpdateEntityAsync(T entity)
        {
            entity.ModifiedAt = DateTime.UtcNow;
            _db.Update(entity);

            bool response = Convert.ToBoolean(await _context.SaveChangesAsync());

            if (!response) return null;

            return entity;
        }
    }
}