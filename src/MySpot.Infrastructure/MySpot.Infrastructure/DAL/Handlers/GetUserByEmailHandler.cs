using System;
using System.Diagnostics;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Core.Repositories;

namespace MySpot.Infrastructure.DAL.Handlers
{
    public class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> HandleAsync(GetUserByEmail query)
        {
            var user = await _userRepository.GetUserByEmail(query.Email);
            return user.AsDto();
        }
    }
}

