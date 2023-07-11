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
		[Theory]
		[InlineData("2023-07-11")]
        [InlineData("2023-07-12")]
        [InlineData("2023-07-13")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
		{
			var now = DateTime.Parse(dateString);
			var invalidDate = now.AddDays(6);
			var weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(now), "P1");
			var reservation = new Reservation(Guid.NewGuid(), Guid.NewGuid(), "John Doe", "XYZ123", new Date(invalidDate));

			var exception = Record.Exception(()=> weeklyParkingSpot.AddReservation(reservation, new Date(now)));

			// Assert.NotNull(exception);
			// Assert.IsType<InvalidReservationDateException>(exception);
			exception.ShouldNotBeNull();
			exception.ShouldBeOfType<InvalidReservationDateException>();
 		}
	}
}

