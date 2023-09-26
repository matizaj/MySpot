using MySpot.Application.Commands;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Application.Services;
using MySpot.Tests.Shared;
using Shouldly;
using Xunit;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Policies;

namespace MySpot.Tests.Services
{
    public class ReservationServiceTests
    {
        private readonly IReservationsService _reservationsService;
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpots;

        public ReservationServiceTests()
        {
            _clock = new TestClock();            
            _weeklyParkingSpots = new InMemoryWeeklyParkingSpot(_clock);
            var _parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
            {
                new ManagerReservationPolicy(),
                new BossReservationPolicy(),
                new RegularEmployeeReservationPolicy(_clock),
            }, _clock);
            _reservationsService = new ReservationsService(_weeklyParkingSpots, _clock, _parkingReservationService);
            
        }

        [Fact]
        public async Task  given_reservation_for_valid_date_create_reservation_should_succeed()
        {
            var parkingSpot = (await _weeklyParkingSpots.GetAllAsync()).First();
            var command = new ReserveParkingSpotForVehicle(parkingSpot.Id, 
                Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(2));

            var id = await _reservationsService.ReserveForVehicleAsync(command);

            id.ShouldNotBeNull();
            id.Value.ShouldBe(command.ReservationId);

        }
    }
}
