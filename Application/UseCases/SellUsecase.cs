using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Contracts;
using Application.Common;
using Application.UsecasesContracts;

namespace Application.UseCases
{
    public class SellUsecase :BaseUsecase<SellEntity>, ISellUsecase
    {
        #region properties
        private readonly ISellRepository _sellRepository;
        #endregion
        #region constructor
        public SellUsecase(ISellRepository sellRepository) : base(sellRepository)
        {           
            _sellRepository = sellRepository;
        }
        #endregion
        #region methods
        public async Task<Result<List<SellEntity>>> GetSellsByCustomerIdAsync(Guid customerId)
        {
            var sells = await _sellRepository.GetSellsByCustomerIdAsync(customerId);
            if (sells == null || sells.Count == 0)
            {
                return Result<List<SellEntity>>.Fail("No sells found for the specified customer.");
            }
            return Result<List<SellEntity>>.Ok(sells);
        }
        public async Task<Result<decimal>> GetTotalSalesAmountAsync(DateTime startDate, DateTime endDate)
        {
            decimal amount = await _sellRepository.GetTotalSalesAmountAsync(startDate, endDate);
            if (amount <= 0)
            {
                return Result<decimal>.Fail("No sales found in the specified date range.");
            }
            return Result<decimal>.Ok(amount);
        }
        public async Task<Result<int>> GetTotalProductsSoldAsync(DateTime startDate, DateTime endDate)
        {
            int quantity = await _sellRepository.GetTotalProductsSoldAsync(startDate, endDate);
            if (quantity <= 0)
            {
                return Result<int>.Fail("No sales found in the specified date range.");
            }
            return Result<int>.Ok(quantity);
        }
        #endregion
    }
}
