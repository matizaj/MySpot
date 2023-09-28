using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        private const string SectionName = "database";
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("database");
            services.Configure<AppOptions>(section);

            var options = configuration.GetOptions<AppOptions>(SectionName);

            services.AddDbContext<MySpotDbContext>(opt =>
            {
                opt.UseNpgsql(options.ConnectionString);
            });
            services.AddScoped<IWeeklyParkingSpotRepository, PostresWeeklyParkingSpotRepository>();
            services.AddScoped<IUnityOfWork, PostgresUnityOfWork>();
            services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandHandlerDecorator<>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddHostedService<DatabaseInitializer>();
            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var option = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(option);

            return option;


        }
    }
}
