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
        private readonly ILogService _logService;
       // private readonly ICacheService _cacheService;
        #endregion
        #region constructor
        public BaseUsecase(IBaseRepository<T> repository,ILogService logService)
        {            
            _repository = repository;
            _logService = logService;
        }
        #endregion
        #region methods
        public async Task<Result<T>> GetByIdAsync(Guid id)
        {
            try
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
                await _logService.InfoAsync($"Fetching {typeof(T).Name} with ID {id}");
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    await _logService.WarnAsync($"Entity of type {typeof(T).Name} with ID {id} not found");
                    return Result<T>.Fail("Entity not found");
                }
                // await _cacheService.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(5));
                await _logService.InfoAsync($"Successfully fetched {typeof(T).Name} with ID {id}");
                return Result<T>.Ok(entity);
            }
            catch(Exception ex)
            {
                await _logService.ErrorAsync($"Unexpected error fetching {typeof(T).Name} with ID {id}: {ex.Message}", ex);
                return Result<T>.Fail("Unexpected error while fetching entity");
            }
        }
        public async Task<Result<List<T>>> GetAllAsync()
        {
            try
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
                await _logService.InfoAsync($"Fetching All {typeof(T).Name}");
                var entities = await _repository.GetAllAsync();
                //  await _cacheService.SetAsync(cacheKey, entities, TimeSpan.FromMinutes(5));
                return Result<List<T>>.Ok(entities);
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync($"Unexpected error fetching {typeof(T).Name}: {ex.Message}", ex);
                return Result<List<T>>.Fail("Unexpected error while fetching entity");
            }
        }
        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                await _logService.InfoAsync($"Removing {typeof(T).Name} with ID {id}");
                await _repository.DeleteAsync(id);
                /*  var entityKey = $"{typeof(T).Name}_{id}";
                  var allKey = $"{typeof(T).Name}_all";

                  await _cacheService.RemoveAsync(entityKey);
                  await _cacheService.RemoveAsync(allKey);*/
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync($"Unexpected error removing {typeof(T).Name}: {ex.Message}", ex);
                return Result.Fail("Unexpected error while deleting entity");
            }
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _logService.InfoAsync($"Adding new {typeof(T).Name} entity");
                var addedEntity = await _repository.AddAsync(entity);
                if (addedEntity == null)
                {
                    await _logService.WarnAsync($"Failed to add new {typeof(T).Name} entity");
                    return Result<T>.Fail("Failed to add entity");
                }
                /*  var entityKey = $"{typeof(T).Name}_{addedEntity.Id}";
                  var allKey = $"{typeof(T).Name}_all";

                  await _cacheService.SetAsync(entityKey, addedEntity, TimeSpan.FromMinutes(5));
                  await _cacheService.RemoveAsync(allKey);*/

                return Result<T>.Ok(addedEntity);
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync($"Unexpected error adding {typeof(T).Name}: {ex.Message}", ex);
                return Result<T>.Fail("Unexpected error while adding entity");
            }
        }
        public async Task<Result> UpdateAsync(T entity)
        {
            try
            {
                await _logService.InfoAsync($"Updating a {typeof(T).Name} entity");
                var addedEntity = await _repository.UpdateAsync(entity);
                if (addedEntity == null)
                {
                    await _logService.InfoAsync($"Failed to Update a {typeof(T).Name} entity with id {typeof(T).GUID}");
                    return Result<T>.Fail("Failed to add entity");
                }
                /*  var entityKey = $"{typeof(T).Name}_{addedEntity.Id}";
               var allKey = $"{typeof(T).Name}_all";

               await _cacheService.SetAsync(entityKey, addedEntity, TimeSpan.FromMinutes(5));
               await _cacheService.RemoveAsync(allKey);*/
                return Result<T>.Ok(addedEntity);
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync($"Unexpected error updating {typeof(T).Name}: {ex.Message}", ex);
                return Result<T>.Fail("Unexpected error while updating entity");
            }
        }
        #endregion
    }
}
