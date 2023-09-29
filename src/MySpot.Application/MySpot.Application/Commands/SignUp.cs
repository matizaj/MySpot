using MySpot.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands
{
    public record SignUp(Guid Id, string Email, string UserName, string Password, string FullName, string Role, DateTime CreatedAt) : ICommand;
   
}
