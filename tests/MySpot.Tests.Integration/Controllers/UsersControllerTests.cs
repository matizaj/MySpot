using MySpot.Application.Commands;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    public class UsersControllerTests:ControllerTestsBase
    {
        [Fact]
        public async Task post_user_should_return_created_201_status_code()
        {
            var sighUpCommand = new SignUp(Guid.Empty, "test-user@test.com", "test-user", "test","test user", "Employee", DateTime.Now);
            var response = await Client.PostAsJsonAsync("api/users", sighUpCommand);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
