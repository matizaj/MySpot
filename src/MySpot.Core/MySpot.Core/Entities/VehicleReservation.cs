using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Entities
{
    public sealed class VehicleReservation : Reservation
    {
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }

        private VehicleReservation()
        {            
        }
        public VehicleReservation(ReservationId id, 
                ParkingSpotId parkingSpotId,                
                EmployeeName employeeName,
                LicensePlate licensePlate,
                Date date) : base(id, parkingSpotId, date)
        {
            EmployeeName = employeeName;
            ChangeLicensePlate(licensePlate);
        }

        public void ChangeLicensePlate(LicensePlate licensePlate)
        {
            LicensePlate = licensePlate;
        }
    }
}
