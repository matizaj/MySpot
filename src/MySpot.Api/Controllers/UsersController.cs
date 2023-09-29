using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Application.Abstractions.ICommandHandler<SignUp> _signupHandler;

        public UsersController(Application.Abstractions.ICommandHandler<SignUp> signupHandler)
        {
            _signupHandler = signupHandler;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            await _signupHandler.HandleAsync(command);
            return NoContent();
        }
    }
}
