﻿using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        IEnumerable<WeeklyParkingSpot> GetAll();
        WeeklyParkingSpot GetById(ParkingSpotId id);
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);
    }
}
