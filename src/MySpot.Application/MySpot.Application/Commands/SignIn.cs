using System;
using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands
{
	public record SignIn(string email, string password):ICommand;
	
}

