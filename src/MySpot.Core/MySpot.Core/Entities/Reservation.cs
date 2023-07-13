using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities
{
    public class Reservation
    {
        public Reservation(ReservationId id, ParkingSpotId parkingSpotId, EmployeeName employeeName, LicensePlate licensePlate, Date date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicensePlate(licensePlate);
            Date = date;            
        }

        public Guid Id { get; }
        public string EmployeeName { get; private set; }
        public Guid ParkingSpotId { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public Date Date { get; private set; }

        public void ChangeLicensePlate(LicensePlate licensePlate)
        {    
            LicensePlate = licensePlate;
        }

    }
}
