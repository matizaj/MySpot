using System;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers
{
    public class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
	{
		public GetUsersHandler()
		{
		}

        public Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        {
            throw new NotImplementedException();
        }
    }
}

