using System;
using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    public class PasswordNotMatchException : CustomException
    {
        public PasswordNotMatchException(string userName) : base($"Invalid password for user {userName}")
        {
        }
    }
}

