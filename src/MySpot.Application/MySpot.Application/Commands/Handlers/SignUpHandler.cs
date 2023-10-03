using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers
{
    internal class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IClock _clock;
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;

        public SignUpHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository)
        {
            _clock = clock;
            _passwordManager = passwordManager;
            _userRepository = userRepository;
        }
        public async Task HandleAsync(SignUp command)
        {
            command = command with { Id = Guid.NewGuid() };
            var userId = new UserId(command.Id);
            var securedPassword = _passwordManager.Secure(command.Password);
            var user = new User(userId, command.Email, command.UserName, securedPassword, command.FullName, command.Role, _clock.Current());
            await _userRepository.CreateUser(user);

        }
    }
}
