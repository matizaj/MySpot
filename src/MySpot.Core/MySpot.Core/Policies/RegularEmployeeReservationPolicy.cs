using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Policies
{
    internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
    {
        private readonly IClock _clock;

        public RegularEmployeeReservationPolicy(IClock clock)
        {
            _clock = clock;
        }

        public bool CanBeApplied(JobTitle jobTitle) => jobTitle == JobTitle.Employee;

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeesReservation = weeklyParkingSpots.SelectMany(x => x.Reservations)
                .Count(x => x.EmployeeName == employeeName);
            return totalEmployeesReservation < 2 && _clock.Current().Hour > 4;
        }
    }
}
