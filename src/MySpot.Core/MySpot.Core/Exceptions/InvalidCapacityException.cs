using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidCapacityException : CustomException
    {
        public InvalidCapacityException(int capacity) : base($"Reservation has invalid capacity value {capacity}")
        {
        }
    }
}
