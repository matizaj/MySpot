using System;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;

namespace MySpot.Application.Queries
{
	public class GetUserByEmail:IQuery<UserDto>
	{
		public string Email { get; set; }
	}
}

