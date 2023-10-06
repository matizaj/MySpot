using MySpot.Infrastructure;
using MySpot.Core;
using MySpot.Application;
using Serilog;
using MySpot.Infrastructure.Logging;
using Microsoft.Extensions.Options;
using MySpot.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.UseSerilog();
var app = builder.Build();
app.UseInfrestructure();

app.MapGet("/", (IOptions<AppOptions> opt) =>
{
    return opt.Value.Name;
});

app.UseApi();

app.Run();
