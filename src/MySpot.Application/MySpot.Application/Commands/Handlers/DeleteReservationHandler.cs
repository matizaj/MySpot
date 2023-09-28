using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
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
    internal class DeleteReservationHandler : ICommandHandler<DeleteReservation>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;

        public DeleteReservationHandler(IWeeklyParkingSpotRepository weeklyParkingSpots)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public async Task HandleAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot == null)
            {
                throw new WeeklyParkingSpotNotFound(command.ReservationId);
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == new ReservationId(command.ReservationId));
            if (existingReservation == null)
            {
                throw new ReservationNotFoundException(command.ReservationId);
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpots.DeleteAsync(weeklyParkingSpot);
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
        {
            var weeklyParkingSpotsByReservation = await _weeklyParkingSpots.GetAllAsync();
            return weeklyParkingSpotsByReservation.SingleOrDefault(x => x.Reservations.Any(x => x.Id == id));
        }
    }
}
