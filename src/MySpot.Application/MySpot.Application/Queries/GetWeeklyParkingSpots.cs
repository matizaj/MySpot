using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Queries
{
    public class GetWeeklyParkingSpots : IQuery<IEnumerable<WeeklyParkingSpotDto>>
    {
        public DateTime? Date { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
