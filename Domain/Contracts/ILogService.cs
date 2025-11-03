using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ILogService
    {
        Task InfoAsync(string message, string? entity = null);
        Task WarnAsync(string message, string? entity = null);
        Task ErrorAsync(string message, Exception? ex = null, string? entity = null);

    }
}
