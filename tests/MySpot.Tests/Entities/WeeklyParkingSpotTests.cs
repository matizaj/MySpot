using System;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Entities
{
	public class WeeklyParkingSpotTests
	{
        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        private readonly Date _date;
        public WeeklyParkingSpotTests()
        {
            _date = new Date(new DateTime(2023, 07, 12));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_date), "P1");
            
        }

        [Theory]
		[InlineData("2023-07-11")]
        [InlineData("2023-07-19")]
        [InlineData("2023-07-18")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
		{
			var invalidDate = DateTime.Parse(dateString);
			var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", new Date(invalidDate));

			var exception = Record.Exception(()=> _weeklyParkingSpot.AddReservation(reservation, _date));

			// Assert.NotNull(exception);
			// Assert.IsType<InvalidReservationDateException>(exception);
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<InvalidReservationDateException>();
 		}

        [Fact]
        public void given_reservation_for_already_taken_date_add_reservation_should_fail()
        {
            var reservationDate = _date.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate);

            _weeklyParkingSpot.AddReservation(reservation, _date);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _date));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        } 
        
        [Fact]
        public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
        {
            var reservationDate = _date.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate);

            _weeklyParkingSpot.AddReservation(reservation, _date);

           _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }
    }
}

