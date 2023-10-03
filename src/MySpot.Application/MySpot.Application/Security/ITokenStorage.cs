using System;
using MySpot.Application.Dtos;

namespace MySpot.Application.Security
{
	public interface ITokenStorage
	{
		void Set(JwtDto jwt);
		JwtDto Get();
	}
}

