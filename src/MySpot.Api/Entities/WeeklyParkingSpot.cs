using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; set; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public void AddReservation(Reservation reservation)
        {
            var isInvalidDate = reservation.Date.Date < From.Date ||
                                reservation.Date.Date > To.Date ||
                                reservation.Date.Date < DateTime.UtcNow.Date;

            if (isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date);
            }

            var reservationAlreadyExist = _reservations.Any(x =>
                x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date);
            }

            _reservations.Add(reservation);
        }

    }
}
