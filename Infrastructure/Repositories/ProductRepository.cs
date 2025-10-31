using Domain.Contracts;
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
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(DBClient client) : base(client) { }

        public async Task<ProductEntity> GetProductByNameAsync(string name)
            => await _set.AsNoTracking()
                         .FirstOrDefaultAsync(p => p.ProductName == name) ?? new ProductEntity();

        public async Task<int> GetStockProduct(Guid id)
        {
            var p = await _set.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return p?.ProductStock ?? -1; 
        }
    }
}
