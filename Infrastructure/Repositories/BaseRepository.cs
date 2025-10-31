using Domain.Entities;
using Infrastructure.DbClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly DBClient _client;
        protected readonly DbSet<T> _set;

        public BaseRepository(DBClient client)
        {
            _client = client;
            _set = _client.Set<T>();          
        }

        public virtual async Task<T> GetByIdAsync(Guid id) => await _set.FindAsync(id);

        public virtual async Task<List<T>> GetAllAsync()
            => await _set.AsNoTracking().ToListAsync();

        public virtual async Task<T> AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _client.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _client.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _set.FindAsync(id);
            if (entity is null) return;
            _set.Remove(entity);
            await _client.SaveChangesAsync();
        }
    }
}
