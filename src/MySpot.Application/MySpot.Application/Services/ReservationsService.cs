using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
        private readonly IClock _clock;

        public ReservationsService(IWeeklyParkingSpotRepository weeklyParkingSpots, IClock clock)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
            _clock = clock;
        }

        public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpots.GetAll().SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                ParkingSpotId = x.ParkingSpotId,
                Date = x.Date.Value.Date,
            });

        public Guid? Create(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = _weeklyParkingSpots.GetById(parkingSpotId);
            if (weeklyParkingSpot == null)
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

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == new ReservationId(command.ReservationId));
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

            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == new ReservationId(command.ReservationId));
            if (existingReservation == null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId id)
        {
            return _weeklyParkingSpots.GetAll().SingleOrDefault(x => x.Reservations.Any(x => x.Id == id));
        }
    }
}
