﻿using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;
using System.Runtime.CompilerServices;
using MySpot.Core.Abstractions;

[assembly: InternalsVisibleTo("MySpot.Tests")]
namespace MySpot.Infrastructure.DAL.Repositories
{
    internal class InMemoryWeeklyParkingSpot : IWeeklyParkingSpotRepository
    {
        private readonly IClock _clock;

        private static List<WeeklyParkingSpot> _weeklyParkingSpots;

        public InMemoryWeeklyParkingSpot(IClock clock)
        {
            _clock = clock;
            _weeklyParkingSpots = new()
            {
                WeeklyParkingSpot.Create(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current()),"P1"),
                WeeklyParkingSpot.Create(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current()),"P2"),
                WeeklyParkingSpot.Create(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current()),"P3"),
                WeeklyParkingSpot.Create(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current()),"P4"),
                WeeklyParkingSpot.Create(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current()),"P5"),
            };
        }
        public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _weeklyParkingSpots.Add(weeklyParkingSpot);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _weeklyParkingSpots.Remove(weeklyParkingSpot);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => Task.FromResult(_weeklyParkingSpots.AsEnumerable());

        public Task<WeeklyParkingSpot> GetByIdAsync(ParkingSpotId id)
        {
            return Task.FromResult(_weeklyParkingSpots.SingleOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot) =>  Task.CompletedTask;

        public Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        {
            return Task.FromResult(_weeklyParkingSpots.AsEnumerable());

        }

    }
}
