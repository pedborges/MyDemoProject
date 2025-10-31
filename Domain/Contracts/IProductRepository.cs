using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Contracts
{
    public interface IProductRepository:IBaseRepository<ProductEntity>
    {
        public Task<ProductEntity> GetProductByNameAsync(string name);
        public Task<int> GetStockProduct(Guid id);
    }
}
