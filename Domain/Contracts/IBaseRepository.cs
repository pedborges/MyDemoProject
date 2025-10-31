using Domain.Entities;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<T> AddAsync(T entity);
    }
}
