using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities
{
    public class Reservation
    {
        public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTime date)
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
        public string LicensePlate { get; private set; }
        public DateTime Date { get; private set; }

        public void ChangeLicensePlate(string licensePlate)
        {
            if(string.IsNullOrEmpty(licensePlate))
            {
                throw new EmptyLicensePlateException();
            }
            LicensePlate = licensePlate;
        }

    }
}
