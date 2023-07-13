using MySpot.Infrastructure;
using MySpot.Core;
using MySpot.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
