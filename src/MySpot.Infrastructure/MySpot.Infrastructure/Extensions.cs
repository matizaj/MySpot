using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>();
            services.AddPostgres();
            return services;
        }
    }
}
