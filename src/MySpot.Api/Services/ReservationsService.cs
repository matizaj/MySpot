using MySpot.Api.Commands;
using MySpot.Api.Dtos;
using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly Clock _clock = new();
        private static readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new() 
        {
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current().AddDays(7)),"P1"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current().AddDays(7)),"P2"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current().AddDays(7)),"P3"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current().AddDays(7)),"P4"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current().AddDays(7)),"P5"),
        };

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
