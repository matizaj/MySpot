using MySpot.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Shared
{
    internal class TestClock : IClock
    {
        public DateTime Current()
        {
            return new DateTime(2023, 07, 13);
        }
    }
}
