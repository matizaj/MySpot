using MySpot.Application.Abstractions;
using System;
namespace MySpot.Application.Commands
{
	public record DeleteReservation(Guid ReservationId): ICommand;
}

