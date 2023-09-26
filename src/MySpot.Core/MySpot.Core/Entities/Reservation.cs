using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public abstract class Reservation
    {
        protected Reservation() { }
        public Reservation(ReservationId id, ParkingSpotId parkingSpotId, Date date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            Date = date;            
        }

        public ReservationId Id { get; }        
        public ParkingSpotId ParkingSpotId { get; private set; }       
        public Date Date { get; private set; }

      

    }
}
