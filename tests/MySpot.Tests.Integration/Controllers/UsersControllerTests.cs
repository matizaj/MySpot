using Microsoft.AspNetCore.Identity;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Core.Entities;
using MySpot.Infrastructure.Security;
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
    public class UsersControllerTests : ControllerTestsBase, IDisposable
    {
        private readonly TestDatabase _testDatabase;
        public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
        {
            _testDatabase = new TestDatabase();
        }

        public void Dispose()
        {
            _testDatabase.Dispose();
        }

        [Fact]
        public async Task post_user_should_return_created_201_status_code()
        {
            var sighUpCommand = new SignUp(Guid.Empty, "test-user@test.com", "test-user", "test","test user", "Employee", DateTime.Now);
            var response = await Client.PostAsJsonAsync("api/users", sighUpCommand);
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task post_sign_in_should_return_200_ok_and_jwt()
        {
            var passwordManager = new PasswordManager(new PasswordHasher<UserDto>());
            var password = "test";
            var securePassword = passwordManager.Secure(password);
            var user = new User(Guid.Empty, "test-user@test.com", "test-user", securePassword, "test user", "Employee", DateTime.Now);
            await _testDatabase.DbContext.Users.AddAsync(user);
            await _testDatabase.DbContext.SaveChangesAsync();

            var command = new SignIn("test-user@test.com", password);
            var response = await Client.PostAsJsonAsync("api/users/sign-in", command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
            jwt.ShouldNotBeNull();
            jwt.AccessToken.ShouldNotBeEmpty();
        }
    }
}
