using MySpot.Application.Abstractions;
using System;
namespace MySpot.Application.Commands
{
	public record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate): ICommand;
}

