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

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(r => r.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpots.GetAllAsync();
            return weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                ParkingSpotId = x.ParkingSpotId,
                Date = x.Date.Value.Date,
            });

        }
        public async Task<Guid?> CreateAsync(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = await _weeklyParkingSpots.GetByIdAsync(parkingSpotId);
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
            await _weeklyParkingSpots.UpdateAsync(weeklyParkingSpot);

            return reservation.Id;
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot =  await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
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
            await _weeklyParkingSpots.UpdateAsync(weeklyParkingSpot);
            return true;
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
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
            await _weeklyParkingSpots.DeleteAsync(weeklyParkingSpot);
            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId id)
        {
            var weeklyParkingSpotsByReservation = await _weeklyParkingSpots.GetAllAsync();
            return weeklyParkingSpotsByReservation.SingleOrDefault(x => x.Reservations.Any(x => x.Id == id));
        }
    }
}
