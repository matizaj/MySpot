using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Abstractions;
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
    public class ChangeReservationLicensePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
        private readonly IClock _clock;

        public ChangeReservationLicensePlateHandler(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpots)
        {
            _clock = clock;
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository weeklyParkingSpots)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public async Task HandleAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                throw new WeeklyParkingSpotNotFound(command.ReservationId);
            }

            var existingReservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.Id == new ReservationId(command.ReservationId));
            if (existingReservation == null)
            {
                throw new ReservationNotFoundException(command.ReservationId);
            }

            if (existingReservation.Date.Value.Date <= _clock.Current())
            {
                throw new CannotUpdateReservationForInvalidDateException(command.LicensePlate);
            }

            existingReservation.ChangeLicensePlate(command.LicensePlate);
            await _weeklyParkingSpots.UpdateAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
        {
            var weeklyParkingSpotsByReservation = await _weeklyParkingSpots.GetAllAsync();
            return weeklyParkingSpotsByReservation.SingleOrDefault(x => x.Reservations.Any(x => x.Id == id));
        }
    }
}
