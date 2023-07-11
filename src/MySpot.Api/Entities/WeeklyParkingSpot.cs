using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
        {
            Id = id;
            Week = week;
            Name = name;
        }

        public Guid Id { get; }
        public Week Week { get; }
        public ParkingSpotName Name { get; set; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public void AddReservation(Reservation reservation, Date now)
        {
            var isInvalidDate = reservation.Date < Week.From ||
                                reservation.Date > Week.To ||
                                reservation.Date < now;

            if (isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date.Value.Date);
            }

            var reservationAlreadyExist = _reservations.Any(x =>
                x.Date == reservation.Date);

            if (reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);
            }

            _reservations.Add(reservation);
        }

        public void RemoveReservation(Guid id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);
            _reservations.Remove(reservation);
        }

    }
}
