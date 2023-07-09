using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySpot.Api.Commands;
using MySpot.Api.Dtos;
using MySpot.Api.Entities;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new() 
        { 
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(7),"P5"),
        };

        public ReservationDto Get(Guid id)=> GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                ParkingSpotId = x.ParkingSpotId,
                Date = x.Date,
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
                command.Date);

            weeklyParkingSpot.AddReservation(reservation);

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

            if (existingReservation.Date <= DateTime.UtcNow.Date)
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicensPlate);
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
