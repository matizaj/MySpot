using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;
        private readonly IClock _clock;
        private readonly IParkingReservationService _parkingReservationService;

        public ReservationsService(IWeeklyParkingSpotRepository weeklyParkingSpots, IClock clock,
            IParkingReservationService parkingReservationService)
        {
            _weeklyParkingSpots = weeklyParkingSpots;
            _clock = clock;
            _parkingReservationService = parkingReservationService;
        }

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(r => r.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpots.GetAllAsync();
            return weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : string.Empty,
                ParkingSpotId = x.ParkingSpotId,
                Date = x.Date.Value.Date,
            });

        }
        public async Task<Guid?> ReserveForVehicleAsync(ReserveParkingSpotForVehicle command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.Current());
            var weeklyParkingSpots = await _weeklyParkingSpots.GetByWeekAsync(week);
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
            if (parkingSpotToReserve == null)
            {
                return default;
            }

            var reservation = new VehicleReservation(
                command.ReservationId,
                command.ParkingSpotId,
                command.EmployeeName,
                command.LicensePlate,
                new Date(command.Date),
                command.Capacity);

           _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);
            await _weeklyParkingSpots.UpdateAsync(parkingSpotToReserve);

            return reservation.Id;
        }

        public async Task<bool> ChangeReservationLicensePlateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot =  await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var existingReservation = weeklyParkingSpot.Reservations
                .OfType<VehicleReservation>()
                .SingleOrDefault(x => x.Id == new ReservationId(command.ReservationId));
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

        public async Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command)
        {
            var week = new Week(command.Date);
            var weeklyPArkingSpots = (await _weeklyParkingSpots.GetByWeekAsync(week)).ToList();
            _parkingReservationService.ReserveParkingForCleaning(weeklyPArkingSpots, new Date(command.Date));

            foreach(var parkingSpot in weeklyPArkingSpots)
            {
                await _weeklyParkingSpots.UpdateAsync(parkingSpot);
            }
        }
    }
}
