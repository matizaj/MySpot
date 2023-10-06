using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;

namespace MySpot.Api
{
    public static class UsersApi
    {
        public  static WebApplication UseApi(this WebApplication app)
        {
            app.MapGet("api/v2/users/me", async (HttpContext context, IQueryHandler<GetUser, UserDto> handler) => {
                var userId = Guid.Parse(context.User.Identity?.Name);
                var userdto = await handler.HandleAsync(new GetUser { Id = userId });
            }).RequireAuthorization().WithName("me");

            app.MapPost("api/v2/users", async (SignUp cmd, ICommandHandler<SignUp> handler) =>
            {
                cmd = cmd with { Id = Guid.NewGuid() };
                await handler.HandleAsync(cmd);
                return Results.CreatedAtRoute("me");
            });
            return app;
        }
    }
}
