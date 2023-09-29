using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Application.Queries.Handlers;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Logging;

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
            services.AddCustomLogger();

            var infraAssembly = typeof(AppOptions).Assembly;
            services.Scan(s => s.FromAssemblies(infraAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            // services.AddScoped<IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>, GetWeeklyParkingSpotHandler>();
            
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
