using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ISellRepository:IBaseRepository<SellEntity>
    {
        public Task<List<SellEntity>> GetSellsByCustomerIdAsync(Guid customerId);
        public Task<decimal> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate);
        public Task<int> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate);
    }
}
