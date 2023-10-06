using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
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
        private const string _password = "test";
        private IUserRepository _repo;
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
            var user = await CreateUser();

            var command = new SignIn(user.Email, _password);
            var response = await Client.PostAsJsonAsync("api/users/sign-in", command);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
            jwt.ShouldNotBeNull();
            jwt.AccessToken.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task get_me_return_user_data_when_request_is_authorize()
        {
            var user = await CreateUser();
            Authorize(user.Id, user.Role, user.Email);
            var userDto = await Client.GetFromJsonAsync<UserDto>("api/users/me");
            userDto.Id.ShouldBe(user.Id.Value);
        }

        private async Task<User> CreateUser()
        {
            var passwordManager = new PasswordManager(new PasswordHasher<UserDto>());
            var securePassword = passwordManager.Secure(_password);
            var user = new User(Guid.Empty, "test-user@test.com", "test-user", securePassword, "test user", "Employee", DateTime.Now);
            await _repo.CreateUser(user);
            //await _testDatabase.DbContext.Users.AddAsync(user);
            //await _testDatabase.DbContext.SaveChangesAsync();
            return user;
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            _repo = new TestUserRepository();
            services.AddSingleton(_repo);
        }
    }
}
