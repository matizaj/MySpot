using Microsoft.AspNetCore.Identity;
using MySpot.Application.Security;
using MySpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.Security
{
    internal class PasswordManager : IPasswordManager
    {
        private readonly IPasswordHasher<UserDto> _passwordHasher;

        public PasswordManager(IPasswordHasher<UserDto> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string Secure(string password)
        {
            return _passwordHasher.HashPassword(default, password);
        }

        public bool Validate(string password, string secretPassword)
        {
            return _passwordHasher.VerifyHashedPassword(default, secretPassword, password)
                is PasswordVerificationResult.Success;
        }
    }
}
