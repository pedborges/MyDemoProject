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
    public interface ISellUsecase: IBaseUsecase<SellEntity>
    {
        Task<Result<List<SellEntity>>> GetSellsByCustomerIdAsync(Guid customerId);
        Task<Result<decimal>> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate);
        Task<Result<int>> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate);
    }
}
