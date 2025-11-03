using Application.DTOs;
using Application.UsecasesContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellController : ControllerBase
    {
        private readonly ISellUsecase _sellUsecase;
        private readonly IProductUsecase _productUsecase;
        private readonly ICustomerUsecase _customerUsecase;

        public SellController(ISellUsecase sellUsecase, IProductUsecase productUsecase, ICustomerUsecase customerUsecase)
        {
            _sellUsecase = sellUsecase;
           _customerUsecase = customerUsecase;
            _productUsecase = productUsecase;
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sellUsecase.GetByIdAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("getAll")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sellUsecase.GetAllAsync();
            return Ok(result.Data);
        }

        [HttpPost("insert")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Add(SellDTO sell)
        {
            SellEntity sellEntity = sell;
            var result = await _sellUsecase.AddAsync(sellEntity);
            if(result.Success)
            {
                if (result.Data == null)
                {
                    return NotFound(result.Error);
                }
                var productResult = await _productUsecase.GetByIdAsync(result.Data.ProductId);
                if (productResult.Success && productResult.Data != null)
                {
                    var product = productResult.Data;
                   
                    if ((product.ProductStock - sell.Quantity) < 0)
                    {
                        return BadRequest("Insufficient stock.");
                    }
                    product.ProductStock -= sell.Quantity;
                    await _productUsecase.UpdateAsync(product);
                }
                if(productResult.Success)
                {
                    // Update customer total purchases
                    var customerResult = await _customerUsecase.GetByIdAsync(sell.CustomerId);
                    if (customerResult.Success && customerResult.Data != null)
                    {
                        var customer = customerResult.Data;
                        customer.Sells.Add(result.Data); 
                        await _customerUsecase.UpdateAsync(customer);
                    }
                }
            }
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpPut("update")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update(SellDTO sell)
        {
            var result = await _sellUsecase.UpdateAsync(sell);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("delete/{id:guid}")]
        [Authorize(Roles = "User,Admin")]        
        public async Task<IActionResult> Delete(Guid id)
        {
           var sell = await _sellUsecase.GetByIdAsync(id);
            if (!sell.Success)
            {
                return BadRequest(sell.Error);
            }
            if (sell.Data== null  )
            {
                return BadRequest(sell.Error);
            }
            var product = await _productUsecase.GetByIdAsync(sell.Data.ProductId);
            if (!product.Success)
            {
                return BadRequest(product.Error);
            }
            if (product.Data == null)
            {
                return BadRequest(product.Error);
            }
            product.Data.ProductStock += sell.Data.Quantity;
            await _productUsecase.UpdateAsync(product.Data);
            var result = await _sellUsecase.DeleteAsync(id);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("by-customer/{customerId:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetByCustomer(Guid customerId)
        {
            var result = await _sellUsecase.GetSellsByCustomerIdAsync(customerId);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("total-amount")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalAmount([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _sellUsecase.GetTotalSalesAmountAsync(startDate, endDate);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("total-products")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalProducts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _sellUsecase.GetTotalProductsSoldAsync(startDate, endDate);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }
    }
}
