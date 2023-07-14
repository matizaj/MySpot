using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            return services.AddDbContext<MySpotDbContext>(opt =>
            {
                const string connectionString = "Host=localhost;Database=MySpot;Username=postgres;Password=";
                opt.UseNpgsql(connectionString);
            });
        }
    }
}
