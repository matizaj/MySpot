﻿using MySpot.Core.Abstractions;

namespace MySpot.Tests.Shared
{
    internal class TestClock : IClock
    {
        public DateTime Current()
        {
            return new DateTime(2023, 9, 26, 12, 0, 0);
        }
    }
}
