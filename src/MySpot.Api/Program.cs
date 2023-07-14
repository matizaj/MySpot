using MySpot.Infrastructure;
using MySpot.Core;
using MySpot.Application;
using MySpot.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db =scope.ServiceProvider.GetRequiredService<MySpotDbContext>();
    db.Database.Migrate();

    var weeklyParkingSpots = db.WeeklyParkingSpots.ToList();
    if(!weeklyParkingSpots.Any())
    {
        var _clock = new Clock();
        weeklyParkingSpots = new()
        {
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current()),"P1"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current()),"P2"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current()),"P3"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current()),"P4"),
                new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current()),"P5"),
        };

        db.WeeklyParkingSpots.AddRange(weeklyParkingSpots);
        db.SaveChanges();
    }
}


app.Run();
