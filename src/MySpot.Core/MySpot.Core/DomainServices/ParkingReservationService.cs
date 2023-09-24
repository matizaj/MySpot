﻿using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Policies;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.DomainServices
{
    public class ParkingReservationService : IParkingReservationService
    {
        private readonly IEnumerable<IReservationPolicy> _policies;
        private readonly IClock _clock;

        public ParkingReservationService(IEnumerable<IReservationPolicy> policies, IClock clock)
        {
            _policies = policies;
            _clock = clock;
        }
        public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpot, 
            JobTitle jobTitle, 
            WeeklyParkingSpot parkingSpotToReserve, 
            Reservation reservation)
        {
            var parkingSpotId = parkingSpotToReserve.Id;
            var policy = _policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));
            if (policy is null)
            {
                throw new NoReservationPolicyFoundException(jobTitle);
            }

            if (policy.CanReserve(allParkingSpot, reservation.EmployeeName) is false) 
            {
                throw new CannotResaerveParkingSpotExcaption(reservation.ParkingSpotId);
            }

            parkingSpotToReserve.AddReservation(reservation, new Date(_clock.Current()));
        }
    }
}
