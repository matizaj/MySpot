using MySpot.Core.Entities;
using MySpot.Application.Services;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;
using System.Runtime.CompilerServices;

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
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current()),"P1"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current()),"P2"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current()),"P3"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current()),"P4"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current()),"P5"),
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
        
    }
}
