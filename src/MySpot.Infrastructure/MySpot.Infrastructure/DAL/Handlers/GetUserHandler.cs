using MySpot.Application.Abstractions;
using MySpot.Application.Queries;
using MySpot.Application.Dtos;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal class GetUserHandler : IQueryHandler<GetUser, UserDto>
    {
        public Task<UserDto> HandleAsync(GetUser query)
        {
            throw new NotImplementedException();
        }
    }
}
