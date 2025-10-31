using Application.Common;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsecasesContracts
{
    public interface IProductUsecase : IBaseUsecase<ProductEntity>
    {
        Task<Result<ProductEntity>> GetProductByNameAsync(string name);
        Task<Result<int>> GetStockProduct(Guid id);
    }
}
