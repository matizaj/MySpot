﻿using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.DomainServices
{
    public interface IParkingReservationService
    {
        void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpot, JobTitle jobTitle, 
            WeeklyParkingSpot parkingSpotToReserve, VehicleReservation reservation);

        void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpot, Date date);
    }
}
