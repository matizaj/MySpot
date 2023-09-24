using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly MySpotDbContext _ctx;

        public PostresWeeklyParkingSpotRepository(MySpotDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            await _ctx.AddAsync(weeklyParkingSpot);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _ctx.Remove(weeklyParkingSpot);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => await _ctx.WeeklyParkingSpots.Include(x=>x.Reservations).ToListAsync();

        public async Task<WeeklyParkingSpot> GetByIdAsync(ParkingSpotId id) 
            => await _ctx.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(s => s.Id == id);

        public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _ctx.Update(weeklyParkingSpot);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        {
            return await _ctx.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .Where(x =>x.Week == week).ToListAsync();

        }
    }
}
