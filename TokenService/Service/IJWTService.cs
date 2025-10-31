using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Service
{
    public interface IJWTService
    {
        public string GenerateToken(string userId,string role, string Issuer, string Audience);
        string? ValidateToken(string token);
    }
}
