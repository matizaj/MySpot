using Humanizer;
using Microsoft.Extensions.Logging;
using MySpot.Application.Abstractions;
using System.Diagnostics;

namespace MySpot.Infrastructure.Logging.Decorators
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {

        private readonly ICommandHandler<T> _handler;
        private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<T> handler, ILogger<LoggingCommandHandlerDecorator<T>> logger)
        {
            _handler = handler;
            _logger = logger;
        }
        public async Task HandleAsync(T command)
        {
            var stopWatch = new Stopwatch();
            var commandName = typeof(T).Name.Underscore();
            stopWatch.Start();
            _logger.LogInformation("Handling a command {commandName}...", commandName);
            await _handler.HandleAsync(command);
            stopWatch.Stop();
            _logger.LogInformation("Completed a command {commandName}... in time {elapsed}", commandName, stopWatch.Elapsed);
        }
    }
}
