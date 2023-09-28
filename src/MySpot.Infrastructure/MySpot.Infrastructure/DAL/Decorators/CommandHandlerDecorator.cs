using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Decorators
{
    internal class CommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly IUnityOfWork _unityOfWork;

        public CommandHandlerDecorator(ICommandHandler<T> handler, IUnityOfWork unityOfWork)
        {
            _handler = handler;
            _unityOfWork = unityOfWork;
        }
        public async Task HandleAsync(T command)
        {
            await _unityOfWork.ExecuteAsync(()=> _handler.HandleAsync(command));
        }
    }
}
