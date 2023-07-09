using System;
namespace MySpot.Api.Commands
{
	public record ChangeReservationLicensePlate(Guid ReservationId, string LicensPlate);
}

