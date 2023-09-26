using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
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

        public ParkingSpotId Id { get; }
        public Week Week { get; }
        public ParkingSpotName Name { get; set; }
        public IEnumerable<Reservation> Reservations => _reservations;

        internal void AddReservation(Reservation reservation, Date now)
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

        public void RemoveReservation(ReservationId id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);
            _reservations.Remove(reservation);
        }
        public void RemoveReservations(IEnumerable<Reservation> reservations)
        {
            var reservation = _reservations
                .RemoveWhere(reservation => reservations.Any(r => r.Id == reservation.Id));
        }

    }
}
