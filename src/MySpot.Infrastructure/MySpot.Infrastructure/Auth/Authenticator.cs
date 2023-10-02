using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.Dtos;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;

namespace MySpot.Infrastructure.Auth
{
    public class Authenticator : IAuthenticator
	{
        private readonly IClock _clock;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly TimeSpan _expiry;
        private readonly SigningCredentials _signinKey;
        private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();

        public Authenticator(IClock clock, IOptions<AuthOptions> options)
        {
            _clock = clock;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
            _expiry = options.Value.Expity ?? TimeSpan.FromHours(1);
            _signinKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigninKey))
                ,SecurityAlgorithms.HmacSha256);
        }
        public JwtDto CreateToken(Guid userId, string role, string email)
        {
            var now = _clock.Current();
            var expires = now.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signinKey);
            var token = _jwtHandler.WriteToken(jwt);
            return new JwtDto { AccessToken=token};
        }
    }
}

