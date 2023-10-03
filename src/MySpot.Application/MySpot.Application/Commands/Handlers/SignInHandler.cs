using System;
using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Repositories;

namespace MySpot.Application.Commands.Handlers
{
    public class SignInHandler : ICommandHandler<SignIn>
	{
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenStorage _token;

        public SignInHandler(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager, ITokenStorage token)
		{
            _userRepository = userRepository;
            _authenticator = authenticator;
            _passwordManager = passwordManager;
            _token = token;
        }

        public async Task HandleAsync(SignIn command)
        {
            var user = await _userRepository.GetUserByEmail(command.email);
            if(user is null)
            {
                throw new UserNotFoundException(command.email);
            }

            if(_passwordManager.Validate(command.password, user.Password) is false)
            {
                throw new PasswordNotMatchException(user.UserName);
            }

            var jwt = _authenticator.CreateToken(user.Id, user.Role, user.Email);
            _token.Set(jwt);
        }
    }
}

