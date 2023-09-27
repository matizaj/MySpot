using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal class GetWeeklyParkingSpotHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
    {
        public Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
        {
            throw new NotImplementedException();
        }
    }
}
