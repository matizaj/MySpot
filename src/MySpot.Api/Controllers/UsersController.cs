using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;
using MySpot.Application.Security;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Application.Abstractions.ICommandHandler<SignUp> _signupHandler;
        private readonly IQueryHandler<GetUser, UserDto> _userQuery;
        private readonly IQueryHandler<GetUserByEmail, UserDto> _userEmailQuery;
        private readonly IAuthenticator _authenticator;
        private readonly Application.Abstractions.ICommandHandler<SignIn> _signinHandler;
        private readonly ITokenStorage _token;

        public UsersController(Application.Abstractions.ICommandHandler<SignUp> signupHandler,
            IQueryHandler<GetUser, UserDto> userQuery,
            IQueryHandler<GetUserByEmail, UserDto> userEmailQuery,
            IAuthenticator authenticator,
            Application.Abstractions.ICommandHandler<SignIn> signinHandler,
            ITokenStorage token)
        {
            _signupHandler = signupHandler;
            _userQuery = userQuery;
            _userEmailQuery = userEmailQuery;
            _authenticator = authenticator;
            _signinHandler = signinHandler;
            _token = token;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            await _signupHandler.HandleAsync(command);
            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn(SignIn command)
        {
            await _signinHandler.HandleAsync(command);
            return Ok(_token.Get());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            return Ok(await _userQuery.HandleAsync(new GetUser() { Id=id}));
        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            var user = await _userEmailQuery.HandleAsync(new GetUserByEmail() { Email = email });
            return Ok(user);
        }

        [HttpGet("jwt")]
        public ActionResult<JwtDto> GetJwt()
        {
            var userId = Guid.NewGuid();
            var token = _authenticator.CreateToken(userId, "user", "test@test.com");
            return Ok(token);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var userId = Guid.Parse(HttpContext.User.Identity?.Name);
            var user = await _userQuery.HandleAsync(new GetUser() { Id = userId });


            return Ok(user);
        }
    }
}
