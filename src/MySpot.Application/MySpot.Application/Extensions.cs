using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Commands.Handlers;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;

namespace MySpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var appAssembly = typeof(ICommandHandler<>).Assembly;
            services.AddSingleton<IClock, Clock>();
            services.Scan(s => s.FromAssemblies(appAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            //services.AddScoped<ICommandHandler<ReserveParkingSpotForVehicle>, ReserveParkingSpotForVehicleHandler>();
            //services.AddScoped<ICommandHandler<ReserveParkingSpotForCleaning>,  ReserveParkingSpotForCleaningHandler>();
            //services.AddScoped<ICommandHandler<DeleteReservation>, DeleteReservationHandler>();
            //services.AddScoped<ICommandHandler<ChangeReservationLicensePlate>,  ChangeReservationLicensePlateHandler>();
            return services;
        }
    }
}
