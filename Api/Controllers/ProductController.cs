using Application.DTOs;
using Application.UsecasesContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductUsecase _productUsecase;

        public ProductController(IProductUsecase productUsecase)
        {
            _productUsecase = productUsecase;
        }

        [HttpGet("getById/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productUsecase.GetByIdAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productUsecase.GetAllAsync();
            return Ok(result.Data);
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] ProductDTO product)
        {
            var result = await _productUsecase.AddAsync(product);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpPut("update")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update([FromBody] ProductDTO product)
        {
            var result = await _productUsecase.UpdateAsync(product);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("delete/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productUsecase.DeleteAsync(id);
            return result.Success? Ok() : BadRequest(result.Error);
        }

        [HttpGet("by-name/{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _productUsecase.GetProductByNameAsync(name);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("stock/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStock(Guid id)
        {
            var result = await _productUsecase.GetStockProduct(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }
    }
}
