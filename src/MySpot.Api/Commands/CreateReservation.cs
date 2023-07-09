using System;
namespace MySpot.Api.Commands
{
	// record behind is a normal class but props has {get; set init;}
	// it means valiue can be initialezed ONLY when it will be created
	public record CreateReservation(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, string LicensePlate, DateTime Date);
}

