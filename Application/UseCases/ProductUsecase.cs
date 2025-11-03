using Application.Common;
using Application.UsecasesContracts;
using Contracts.Cache;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.UseCases
{
    public class ProductUsecase: BaseUsecase<ProductEntity>, IProductUsecase
    {
        #region properties
        private readonly IProductRepository _productRepository;
        private readonly ILogService _logger;
        // private readonly ICacheService _cache;
        #endregion
        #region constructor
        public ProductUsecase(IProductRepository productRepository, ILogService logger) : base(productRepository, logger)
        {
            _productRepository = productRepository;
            _logger = logger;
            // _cache = cache;
        }
        #endregion
        #region methods
        public async Task<Result<ProductEntity>> GetProductByNameAsync(string name)
        {
            try 
            {
            await _logger.InfoAsync($"Searching for product with name '{name}'.");
            var product = await _productRepository.GetProductByNameAsync(name);
            if (product == null || product.ProductName == string.Empty)
            {
                await _logger.WarnAsync($" Product with name '{name}' not found.");
                return  Result<ProductEntity>.Fail("Product not found.");
            }
                await _logger.InfoAsync($" Successfully found product '{product.ProductName}'.");
                return Result<ProductEntity>.Ok(product);
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync($" Failed to fetch product with name '{name}'.", ex);
                return Result<ProductEntity>.Fail("An error occurred while fetching the product.");
            }
        }
        public async Task<Result<int>> GetStockProduct(Guid id)
        {
            try
            {
                var stock = await _productRepository.GetStockProduct(id);
                if (stock < 0)
                {
                    return Result<int>.Fail("Product not found.");
                }
                return Result<int>.Ok(stock);
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync($"❌ Failed to retrieve stock for product ID {id}.", ex);
                return Result<int>.Fail("An error occurred while checking product stock.");
            }
        }
        #endregion
    }
}
