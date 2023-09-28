using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Exceptions
{
    internal sealed class CannotUpdateReservationForInvalidDateException : CustomException
    {
        public CannotUpdateReservationForInvalidDateException(string licensePlate) 
            : base($"Cannot update license plate {licensePlate} for past reservation")
        {
            LicensePlate = licensePlate;
        }

        public string LicensePlate { get; }
    }
}
