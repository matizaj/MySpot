using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Exceptions;

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
            services.AddSingleton<ExceptionMiddleware>();
            return services;
        }

        public static WebApplication UseInfrestructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            return app;
        }
    }
}
