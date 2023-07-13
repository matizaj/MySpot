namespace MySpot.Core.Exceptions
{
    public class InvalidReservationDateException : CustomException
    {
        public InvalidReservationDateException(DateTime date) : base($"Reservation date {date:d} is invalid")
        {
        }
    }
}
