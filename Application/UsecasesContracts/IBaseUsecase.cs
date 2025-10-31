using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsecasesContracts
{
    public interface IBaseUsecase<T> where T : Domain.Entities.BaseEntity
    {
        Task<Result<T>> GetByIdAsync(Guid id);
        Task<Result<List<T>>> GetAllAsync();
        Task<Result<T>> AddAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(Guid id);
    }
}
