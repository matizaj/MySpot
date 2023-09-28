using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal class GetWeeklyParkingSpotHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
    {
        private readonly MySpotDbContext _context;

        public GetWeeklyParkingSpotHandler(MySpotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
        {
            var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
            var weeklyParkingSpots = await _context.WeeklyParkingSpots
                .Where(x => week == null || x.Week == week)
                .Include(x =>x.Reservations)
                .AsNoTracking()
                .ToListAsync();

            return weeklyParkingSpots.Select(x => x.AsDto());
        }
    }
}
