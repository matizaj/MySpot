using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySpot.Api.Entities;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new() 
        { 
            new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P1"),
            new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P2"),
            new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P3"),
            new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P4"),
            new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P5"),
        };

        public Reservation Get(Guid id)=> GetAllWeekly().SingleOrDefault(x => x.Id == id);
          
        public IEnumerable<Reservation> GetAllWeekly() => _weeklyParkingSpots.SelectMany(x => x.Reservations);

        public Guid? Create(Reservation reservation)
        {
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x=>x.Id == reservation.ParkingSpotId);
            if(weeklyParkingSpot == null)
            {
                return default;
            }

            weeklyParkingSpot.AddReservation(reservation);
            reservation.
        }

        public bool Update(int id, Reservation reservation)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == reservation.ParkingSpotId);
            if (existingReservation == null)
            {
                return false;
            }

            if(existingReservation.Date <= DateTime.UtcNow.Date)
            {
                return false;
            }

            existingReservation.LicensePlate = reservation.LicensePlate;
            return true;
        }
        public bool Delete(int id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);
            if (reservation == null)
            {
                return false;
            }
            _reservations.Remove(reservation);
            return true;
        }
    }
}
