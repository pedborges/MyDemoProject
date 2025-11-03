using Domain.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Log
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public Task InfoAsync(string message)
        {
            _logger.LogInformation("[App] {Message}", message);
            return Task.CompletedTask;
        }

        public Task WarnAsync(string message )
        {
            _logger.LogWarning("[App] {Message}", message);
            return Task.CompletedTask;
        }

        public Task ErrorAsync(string message, Exception? ex = null)
        {
            _logger.LogError(ex, "[App] {Message}", message);
            return Task.CompletedTask;
        }
    }
}
