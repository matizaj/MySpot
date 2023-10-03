using System;
using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public class UserNotFoundException : CustomException
	{
        public UserNotFoundException(string email) : base($"User with email {email} not exist")
        {
            Email = email;
        }

        public string Email { get; }
    }
}

