using MySpot.Api.Commands;
using MySpot.Api.Dtos;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;
        private static readonly Clock _clock = new();

        public ReservationsService(List<WeeklyParkingSpot> weeklyParkingSpots)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
        }

        public ReservationDto Get(Guid id)=> GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                ParkingSpotId = x.ParkingSpotId,
                Date = x.Date.Value.Date,
            });

        public Guid? Create(CreateReservation command)
        {
            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x=>x.Id == command.ParkingSpotId);
            if(weeklyParkingSpot == null)
            {
                return default;
            }

            var reservation = new Reservation(
                command.ReservationId,
                command.ParkingSpotId,
                command.EmployeeName,
                command.LicensePlate,
                new Date(command.Date));

            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));

            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot == null)
            {
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
            if (existingReservation == null)
            {
                return false;
            }

            if (existingReservation.Date.Value.Date <= _clock.Current())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicensePlate);
            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (weeklyParkingSpot == null)
            {
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
            if (existingReservation == null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid id)
        {
            return _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(x => x.Id == id));
        }
    }
}
