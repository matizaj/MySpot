using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions
{
    internal class ReservationNotFoundException : CustomException
    {
        public ReservationNotFoundException(Guid id) : base($"Reservation wit id {id} was not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
