namespace MySpot.Api.Exceptions
{
    public class ParkingSpotAlreadyReservedException : CustomException
    {
        public ParkingSpotAlreadyReservedException(string name, DateTime date) : base($"Parking spot {name} already taken {date}")
        {
        }
    }
}
