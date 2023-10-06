using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Integration
{
    internal class TestUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public Task CreateUser(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task<User> GetUserByEmail(Email email)
        {
            return Task.FromResult(_users.SingleOrDefault(x => x.Email == email));
        }

        public Task<User> GetUserById(UserId id)
        {
            return Task.FromResult(_users.SingleOrDefault(x => x.Id == id));
        }

        public Task<User> GetUserByUsername(UserName userName)
        {
            return Task.FromResult(_users.SingleOrDefault(x => x.UserName == userName));
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return Task.FromResult(_users.AsEnumerable());
        }
    }
}
