using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.Security;
using MySpot.Infrastructure.DAL;

namespace MySpot.Infrastructure.Auth
{
	internal static class Extensions
	{
		private const string SectionName = "auth";
		public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IAuthenticator, Authenticator>();
			services.Configure<AuthOptions>(configuration.GetSection(SectionName));
			var options = configuration.GetOptions<AuthOptions>(SectionName);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.Audience = options.Audience;
				x.IncludeErrorDetails = true;
				x.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = options.Issuer,
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigninKey))
				};
			});
			services.AddAuthorization(auth =>
			{
				auth.AddPolicy("is-admin", policy =>
				{
					policy.RequireRole("admin");
				});
			});
			services.AddScoped<ITokenStorage, HttpContextTokenStorage>();
			return services;
		}
	}
}

