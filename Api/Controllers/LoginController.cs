using Application.Common;
using Application.DTOs;
using Application.UsecasesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenService.Service;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class LoginController:ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly ICustomerUsecase _customerUsecase;
        public LoginController(IJWTService jwtService, ICustomerUsecase customer)
        {
            _jwtService = jwtService;
            _customerUsecase = customer;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _customerUsecase.ValidateLogin(dto);

            if (!result.Success || result.Data == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(
                result.Data.Id.ToString(),
                result.Data.CustomerRole,
                "MYDemoProjectURL",
                "MyDemoProjectDestination"
            );
            return Ok(new { Token = token });
        }
    }
}
