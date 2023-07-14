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
        public void Add(WeeklyParkingSpot weeklyParkingSpot)
        {
            _ctx.Add(weeklyParkingSpot);
            _ctx.SaveChanges();
        }

        public void Delete(WeeklyParkingSpot weeklyParkingSpot)
        {
            _ctx.Remove(weeklyParkingSpot);
            _ctx.SaveChanges();
        }

        public IEnumerable<WeeklyParkingSpot> GetAll() => _ctx.WeeklyParkingSpots.Include(x=>x.Reservations).ToList();

        public WeeklyParkingSpot GetById(ParkingSpotId id) 
            => _ctx.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefault(s => s.Id == id);

        public void Update(WeeklyParkingSpot weeklyParkingSpot)
        {
            _ctx.Update(weeklyParkingSpot);
            _ctx.SaveChanges();
        }
    }
}
