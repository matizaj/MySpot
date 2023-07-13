using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Services
{
    public class ReservationServiceTests
    {
        private readonly ReservationsService _reservationsService;
        private readonly static Clock _clock = new Clock();
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current()),"P1"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current()),"P2"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current()),"P3"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current()),"P4"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current()),"P5"),
        };

        public ReservationServiceTests()
        {
            _reservationsService = new ReservationsService(_weeklyParkingSpots);
        }

        [Fact]
        public void given_reservation_for_valid_date_create_reservation_should_succeed()
        {
            var parkingSpot = _weeklyParkingSpots.First();
            var command = new CreateReservation(parkingSpot.Id, 
                Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

            var id = _reservationsService.Create(command);

            id.ShouldNotBeNull();
            id.Value.ShouldBe(command.ReservationId);

        }
    }
}
