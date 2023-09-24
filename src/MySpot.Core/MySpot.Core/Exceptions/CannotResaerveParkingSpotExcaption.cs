using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class CannotResaerveParkingSpotExcaption : CustomException
    {
        public CannotResaerveParkingSpotExcaption(ParkingSpotId id) : base($"Cannot reserve parking spot {id}")
        {
            Id = id;
        }

        public ParkingSpotId Id { get; }
    }
}
