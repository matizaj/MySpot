﻿using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
        Task<WeeklyParkingSpot> GetByIdAsync(ParkingSpotId id);
        Task AddAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot);
    }
}
