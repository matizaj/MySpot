using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;

namespace MySpot.Application.Queries
{
    public class GetUser : IQuery<UserDto>
    {
        public Guid Id { get; set; }
    }

}
