using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Dtos;
using MySpot.Application.Security;
using MySpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.Security
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<UserDto>, PasswordHasher<UserDto>>();
            services.AddSingleton<IPasswordManager, PasswordManager>();
            return services;
        }
    }
}
