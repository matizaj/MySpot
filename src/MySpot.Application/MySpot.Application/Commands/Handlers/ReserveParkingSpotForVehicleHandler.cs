using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Commands.Handlers
{
    internal class ReserveParkingSpotForVehicleHandler : ICommandHandler<ReserveParkingSpotForVehicle>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
        private readonly IClock _clock;
        private readonly IParkingReservationService _parkingReservationService;

        public ReserveParkingSpotForVehicleHandler(IWeeklyParkingSpotRepository weeklyParkingSpots, IClock clock, IParkingReservationService parkingReservationService)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
            _clock = clock;
            _parkingReservationService = parkingReservationService;
        }

        public async Task HandleAsync(ReserveParkingSpotForVehicle command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.Current());
            var weeklyParkingSpots = await _weeklyParkingSpots.GetByWeekAsync(week);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
            if (parkingSpotToReserve == null)
            {
                throw new WeeklyParkingSpotNotFound(parkingSpotId);
            }

            var reservation = new VehicleReservation(
                command.ReservationId,
                command.ParkingSpotId,
                command.EmployeeName,
                command.LicensePlate,
                new Date(command.Date),
                command.Capacity);

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);
            await _weeklyParkingSpots.UpdateAsync(parkingSpotToReserve);
        }
    }
}
