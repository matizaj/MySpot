
using Microsoft.AspNetCore.Http;
using MySpot.Application.Dtos;
using MySpot.Application.Security;

namespace MySpot.Infrastructure.Auth
{
    public class HttpContextTokenStorage : ITokenStorage
	{

        public string Token { get; private set; }
        public HttpContextTokenStorage()
        {
        }
        public JwtDto Get()
        {
            return new JwtDto() { AccessToken = Token };
        }

        public void Set(JwtDto jwt)
        {
            Token = jwt.AccessToken;
        }
    }
}

