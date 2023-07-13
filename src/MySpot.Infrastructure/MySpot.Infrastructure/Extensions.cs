using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>();
            return services;
        }
    }
}
