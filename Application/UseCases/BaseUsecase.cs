using Application.Common;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Contracts.Cache;

namespace Application.UseCases
{
    public abstract class BaseUsecase<T> where T : BaseEntity
    {
        #region properties
        private readonly IBaseRepository<T> _repository;
       // private readonly ICacheService _cacheService;
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
            /* var cacheKey = $"{typeof(T).Name}_{id}";
            if (cacheKey != null)
            {
                var cachedEntity = await _cacheService.GetAsync<T>(cacheKey);
                if (cachedEntity != null)
                {
                    return Result<T>.Ok(cachedEntity);
                }
            }*/
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Result<T>.Fail("Entity not found");
            }
           // await _cacheService.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(5));
            return Result<T>.Ok(entity);
        }
        public async Task<Result<List<T>>> GetAllAsync()
        {
            /* var cacheKey = $"{typeof(T).Name}";
            if (cacheKey != null)
            {
                var cachedEntity = await _cacheService.GetAsync<List<T>>(cacheKey);
                if (cachedEntity != null)
                {
                    return Result<List<T>>.Ok(cachedEntity);
                }
            }*/
            var entities = await _repository.GetAllAsync();
           //  await _cacheService.SetAsync(cacheKey, entities, TimeSpan.FromMinutes(5));
            return Result<List<T>>.Ok(entities);
        }
        public async Task<Result> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
          /*  var entityKey = $"{typeof(T).Name}_{id}";
            var allKey = $"{typeof(T).Name}_all";

            await _cacheService.RemoveAsync(entityKey);
            await _cacheService.RemoveAsync(allKey);*/
            return Result.Ok();
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            var addedEntity = await _repository.AddAsync(entity);
            if (addedEntity == null)
            {
                return Result<T>.Fail("Failed to add entity");
            }
          /*  var entityKey = $"{typeof(T).Name}_{addedEntity.Id}";
            var allKey = $"{typeof(T).Name}_all";

            await _cacheService.SetAsync(entityKey, addedEntity, TimeSpan.FromMinutes(5));
            await _cacheService.RemoveAsync(allKey);*/

            return Result<T>.Ok(addedEntity);
        }
        public async Task<Result> UpdateAsync(T entity)
        {
            var addedEntity = await _repository.UpdateAsync(entity);
            if (addedEntity == null)
            {
                return Result<T>.Fail("Failed to add entity");
            }
            /*  var entityKey = $"{typeof(T).Name}_{addedEntity.Id}";
           var allKey = $"{typeof(T).Name}_all";

           await _cacheService.SetAsync(entityKey, addedEntity, TimeSpan.FromMinutes(5));
           await _cacheService.RemoveAsync(allKey);*/
            return Result<T>.Ok(addedEntity);
        }
        #endregion
    }
}
