﻿using MySpot.Core.Abstractions;

namespace MySpot.Application.Services
{
    public class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}

