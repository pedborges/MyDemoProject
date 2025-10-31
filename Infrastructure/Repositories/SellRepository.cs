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
    public class SellRepository : BaseRepository<SellEntity>, ISellRepository
    {
        public SellRepository(DBClient client) : base(client) { }

        public async Task<List<SellEntity>> GetSellsByCustomerIdAsync(Guid customerId)
            => await _set.AsNoTracking()
                         .Where(s => s.CustomerId == customerId)
                         .ToListAsync();

        public async Task<decimal> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate)
            => await _set.AsNoTracking()
                         .Where(s => s.SellDate >= startDate && s.SellDate <= endDate)
                         .SumAsync(s => s.TotalPrice);

        public async Task<int> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate)
            => await _set.AsNoTracking()
                         .Where(s => s.SellDate >= startDate && s.SellDate <= endDate)
                         .SumAsync(s => s.Quantity);

    }
}
