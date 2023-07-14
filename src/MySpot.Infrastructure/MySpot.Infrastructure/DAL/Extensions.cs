using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
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
           services.AddDbContext<MySpotDbContext>(opt =>
            {
                const string connectionString = "Host=localhost;Database=MySpot;Username=postgres;Password=";
                opt.UseNpgsql(connectionString);
            });
            services.AddScoped<IWeeklyParkingSpotRepository, PostresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return services;
        }
    }
}
