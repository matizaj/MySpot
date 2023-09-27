using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class ParkingSpotCapacityExceededException : CustomException
    {
        public ParkingSpotCapacityExceededException(Guid id, DateTimeOffset date, int capacity ) 
            : base($"Reservation {id} for date {date} exceed maximal capatity and hac {capacity}")
        {
        }
    }
}
