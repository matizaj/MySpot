using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.DAL.Decorators
{
    internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;

        public LoggingCommandHandlerDecorator(ICommandHandler<T> handler)
        {
            _handler = handler;
        }
        public async Task HandleAsync(T command)
        {
            Console.Out.WriteLineAsync($"Operation of type {typeof(T)}");
            await _handler.HandleAsync(command);
        }
    }
}
