using Application.DTOs;
using Application.UsecasesContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerUsecase _customerUsecase;

        public CustomerController(ICustomerUsecase customerUsecase)
        {
            _customerUsecase = customerUsecase;
        }

        [HttpGet("getById/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _customerUsecase.GetByIdAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Error);
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerUsecase.GetAllAsync();
            if (result.Success)
            {
                List<CustomerDTO> customers = new List<CustomerDTO>();
                foreach (var customer in result.Data)
                {
                    CustomerDTO dto = customer;
                    customers.Add(dto);
                }
                return Ok(customers);
            }
            return  NotFound(result.Error);
        }
        [HttpGet("getAllWithSells")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllWithSells()
        {
            var result = await _customerUsecase.GetCustomerWithSells();
            if (result.Success)
            {
                List<CustomerDTO> customers = new List<CustomerDTO>();
                foreach (var customer in result.Data)
                {
                    CustomerDTO dto = customer;
                    customers.Add(dto);
                }
                return Ok(customers);
            }
            return NotFound(result.Error);
        }

        [HttpPost("insert")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(CustomerDTO customer)
        {
            var result = await _customerUsecase.AddAsync(customer);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpPut("update")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update(CustomerDTO customer)
        {
            var result = await _customerUsecase.UpdateAsync(customer);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("delete/{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customerUsecase.DeleteAsync(id);
            return result.Success ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("check-email/{email}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CheckEmailUnique(string email)
        {
            var result = await _customerUsecase.IsEmailUniqueAsync(email);
            return result.Success ? Ok("E-mail available.") : BadRequest(result.Error);
        }

        [HttpGet("birthdays-today")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BirthdayVerification()
        {
            var result = await _customerUsecase.BirthdayVerification();
           
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            List<CustomerDTO> customers = new List<CustomerDTO>();
            foreach (var customer in result.Data)
            {
                CustomerDTO dto = customer;
                customers.Add(dto);
            }
            return Ok(customers);
        }

        [HttpGet("by-city/{city}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCity(string city)
        {
            var result = await _customerUsecase.GetCustomersByCityAsync(city);
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            List<CustomerDTO> customers = new List<CustomerDTO>();
            foreach (var customer in result.Data)
            {
                CustomerDTO dto = customer;
                customers.Add(dto);
            }
            return Ok(customers);
            
        }
    }

}
