using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpot>();
            var section = configuration.GetSection("app");
            services.Configure<AppOptions>(section);
            services.AddPostgres(configuration);
            return services;
        }
    }
}
