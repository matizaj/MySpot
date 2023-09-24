using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    internal sealed class NoReservationPolicyFoundException : CustomException
    {
        public NoReservationPolicyFoundException(JobTitle jobTitle) : base($"No reservation policy for {jobTitle} has been found" )
        {
            JobTitle = jobTitle;
        }

        public JobTitle JobTitle { get; }
    }
}
