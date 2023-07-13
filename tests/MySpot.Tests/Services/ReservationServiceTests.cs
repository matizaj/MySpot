using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Tests.Shared;
using Shouldly;
using Xunit;

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
            _reservationsService = new ReservationsService(_weeklyParkingSpots, _clock);
        }

        [Fact]
        public void given_reservation_for_valid_date_create_reservation_should_succeed()
        {
            var parkingSpot = _weeklyParkingSpots.GetAll().First();
            var command = new CreateReservation(parkingSpot.Id, 
                Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

            var id = _reservationsService.Create(command);

            id.ShouldNotBeNull();
            id.Value.ShouldBe(command.ReservationId);

        }
    }
}
