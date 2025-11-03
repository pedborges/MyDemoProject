using Domain.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Log
{
    public class LogService<T> : ILogService<T>
    {
        private readonly ILogger<T> _logger;

        public LogService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public Task InfoAsync(string message, string? entity = null)
        {
            _logger.LogInformation("[{Entity}] {Message}", entity ?? "App", message);
            return Task.CompletedTask;
        }

        public Task WarnAsync(string message, string? entity = null)
        {
            _logger.LogWarning("[{Entity}] {Message}", entity ?? "App", message);
            return Task.CompletedTask;
        }

        public Task ErrorAsync(string message, Exception? ex = null, string? entity = null)
        {
            _logger.LogError(ex, "[{Entity}] {Message}", entity ?? "App", message);
            return Task.CompletedTask;
        }
    }
}
