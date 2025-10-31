using Application.Common;
using Application.UsecasesContracts;
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
        #endregion
        #region constructor
        public ProductUsecase(IProductRepository productRepository):base(productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion
        #region methods
        public async Task<Result<ProductEntity>> GetProductByNameAsync(string name)
        {
            var product = await _productRepository.GetProductByNameAsync(name);
            if (product == null || product.ProductName == string.Empty)
            {

                return  Result<ProductEntity>.Fail("Product not found.");
            }
            return Result<ProductEntity>.Ok(product);
        }
        public async Task<Result<int>> GetStockProduct(Guid id)
        {
            var stock = await _productRepository.GetStockProduct(id);
            if (stock < 0)
            {
                return Result<int>.Fail("Product not found.");
            }
            return Result<int>.Ok(stock);

        }
        #endregion
    }
}
