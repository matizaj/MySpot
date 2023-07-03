using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySpot.Api.NewFolder;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<Reservation> _reservations = new();
        private static int _id = 1;
        private static readonly List<string> _parkingSpotName = new() { "P1", "P2", "P3", "P4", "P5" };

        public Reservation Get(int id)=> _reservations.SingleOrDefault(x => x.Id == id);
          
        public IEnumerable<Reservation> GetAll() => _reservations;

        public int? Create(Reservation reservation)
        {
            if (_parkingSpotName.All(x => x != reservation.ParkingSpotName))
            {
                return null;
            }
            reservation.Date = DateTime.UtcNow.AddDays(1).Date;

            var reservationAlreadyExist = _reservations.Any(x =>
                x.ParkingSpotName == reservation.ParkingSpotName &&
                x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExist)
            {
                return null;
            }

            reservation.Id = _id;

            _id++;
            _reservations.Add(reservation);
            return reservation.Id;
        }

        public bool Update(int id, Reservation reservation)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation == null)
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
