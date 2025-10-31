using Application.Common;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.UseCases
{
    public abstract class BaseUsecase<T> where T : BaseEntity
    {
        #region properties
        private readonly IBaseRepository<T> _repository;
        #endregion
        #region constructor
        public BaseUsecase(IBaseRepository<T> repository)
        {            
            _repository = repository;
        }
        #endregion
        #region methods
        public async Task<Result<T>> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Result<T>.Fail("Entity not found");
            }
            return Result<T>.Ok(entity);
        }
        public async Task<Result<List<T>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return Result<List<T>>.Ok(entities);
        }
        public async Task<Result> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return Result.Ok();
        }
        public async Task<Result<T>> AddAsync(T entity)
        {
            var addedEntity = await _repository.AddAsync(entity);
            if (addedEntity == null)
            {
                return Result<T>.Fail("Failed to add entity");
            }
            return Result<T>.Ok(addedEntity);
        }
        public async Task<Result> UpdateAsync(T entity)
        {
            var addedEntity = await _repository.UpdateAsync(entity);
            if (addedEntity == null)
            {
                return Result<T>.Fail("Failed to add entity");
            }
            return Result<T>.Ok(addedEntity);
        }
        #endregion
    }
}
