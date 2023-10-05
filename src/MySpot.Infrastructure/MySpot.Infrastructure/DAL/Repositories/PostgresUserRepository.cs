using System;
using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal class PostgresUserRepository : IUserRepository
	{
        private readonly MySpotDbContext _ctx;

        public PostgresUserRepository(MySpotDbContext ctx)
		{
            _ctx = ctx;
        }

        public async Task CreateUser(User user)
        {
            await _ctx.AddAsync(user);
        }

        public async Task<User> GetUserByEmail(Email email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserById(UserId id)
        {
            return await _ctx.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(UserName userName)
        {
            return await _ctx.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _ctx.Users.ToListAsync();
        }
    }
}

