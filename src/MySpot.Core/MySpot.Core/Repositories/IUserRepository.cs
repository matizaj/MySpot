using System;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetUserById(UserId id);
		Task<IEnumerable<User>> GetUsers();
		Task<User> GetUserByEmail(Email email);
		Task<User> GetUserByUsername(UserName userName);
		Task CreateUser(User user);
	}
}

