﻿using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers
{
    internal class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
        private readonly IParkingReservationService _parkingReservationService;

        public ReserveParkingSpotForCleaningHandler(IParkingReservationService parkingReservationService, IWeeklyParkingSpotRepository weeklyParkingSpots)
        {
            _parkingReservationService = parkingReservationService;
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public ReserveParkingSpotForCleaningHandler(IWeeklyParkingSpotRepository weeklyParkingSpots)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public async Task HandleAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.Date);
            var weeklyPArkingSpots = (await _weeklyParkingSpots.GetByWeekAsync(week)).ToList();
            _parkingReservationService.ReserveParkingForCleaning(weeklyPArkingSpots, new Date(command.Date));

            foreach (var parkingSpot in weeklyPArkingSpots)
            {
                await _weeklyParkingSpots.UpdateAsync(parkingSpot);
            }
        }
    }
}
