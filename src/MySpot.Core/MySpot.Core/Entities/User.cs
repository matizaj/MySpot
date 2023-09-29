using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Entities
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; private set; }

        public UserDto(Guid id, string email, string userName, string password, string fullName, string role, DateTime createdAt)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Password = password;
            FullName = fullName;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}
