using MySpot.Application.Dtos;
using MySpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Handlers
{
    internal static class Extensions
    {
        public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
        {
            return new WeeklyParkingSpotDto()
            {
                Id = entity.Id.Value.ToString(),
                Name = entity.Name,
                Capacity = entity.Capacity,
                From = entity.Week.From.Value.Date,
                To = entity.Week.To.Value.Date,
                Reservations = entity.Reservations.Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : string.Empty,
                    Date = x.Date.Value.Date,
                    Type = x is VehicleReservation ? nameof(VehicleReservation) : nameof(CleaningReservation),
                })
            };
        }

        public static UserDto AsDto(this User entity)
        {
            var dto = new UserDto()
            {
                Id = entity.Id,
                FullName = entity.FullName,
                UserName = entity.UserName,
                Role = entity.Role
            };
            return dto;
        }
    }
}
