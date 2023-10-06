using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySpot.Application.Dtos;
using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Infrastructure;
using MySpot.Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Integration.Controllers
{
    [Collection("api")]
    public abstract class ControllerTestsBase : IClassFixture<OptionsProvider>
    {
        private Authenticator _authenticator;

        protected HttpClient Client { get; }
        protected JwtDto Authorize(Guid userId, string role, string email)
        {
            var jwt = _authenticator.CreateToken(userId, role, email);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
            return jwt;
        }
        public ControllerTestsBase(OptionsProvider optionsProvider)
        {
            var app =  new MySpotTestApp(ConfigureServices);
            Client = app.Client;
            var authOptions = optionsProvider.Get<AuthOptions>("auth");
            _authenticator = new Authenticator(new Clock(), new OptionsWrapper<AuthOptions>(authOptions));
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
