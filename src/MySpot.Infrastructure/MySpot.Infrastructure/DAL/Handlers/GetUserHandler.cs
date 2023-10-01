using MySpot.Application.Abstractions;
using MySpot.Application.Queries;
using MySpot.Application.Dtos;
using MySpot.Core.Repositories;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal class GetUserHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> HandleAsync(GetUser query)
        {
            var user = await _userRepository.GetUserById(query.Id);
            var userDto = user.AsDto();
            return userDto;
        }
    }
}
