using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ILogService
    {
        Task InfoAsync(string message);
        Task WarnAsync(string message);
        Task ErrorAsync(string message, Exception? ex = null);

    }
}
