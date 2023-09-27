using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Exceptions
{
    internal sealed class WeeklyParkingSpotNotFound : CustomException
    {
        public WeeklyParkingSpotNotFound(Guid id) : base($"Weekly parking spot with id {id} was not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
