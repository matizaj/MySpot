using MySpot.Application.Abstractions;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands.Handlers
{
    internal class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IClock _clock;
        private readonly IPasswordManager _passwordManager;

        public SignUpHandler(IClock clock, IPasswordManager passwordManager)
        {
            _clock = clock;
            _passwordManager = passwordManager;
        }
        public async Task HandleAsync(SignUp command)
        {
            command = command with { Id = Guid.NewGuid() };
            var securedPassword = _passwordManager.Secure(command.Password);
            var user = new UserDto(command.Id, command.Email, command.UserName, securedPassword, command.FullName, command.Role, _clock.Current());

        }
    }
}
