using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Entities
{
    public class CleaningReservation : Reservation
    {
        public CleaningReservation(ReservationId id, ParkingSpotId parkingSpotId, Date date) : base(id, parkingSpotId, date)
        {
        }
    }
}
