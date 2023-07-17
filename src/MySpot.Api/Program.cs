using MySpot.Infrastructure;
using MySpot.Core;
using MySpot.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
