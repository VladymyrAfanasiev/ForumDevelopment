using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthorizationServiceDevelopment.Models.Configurations;
using AuthorizationServiceDevelopment.Models.Tokens;
using AuthorizationServiceDevelopment.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationServiceDevelopment.Services.Authentication
{
	public class AccessTokenGenerator
	{
		private readonly AuthenticationConfiguration configuration;

		public AccessTokenGenerator(AuthenticationConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public AccessToken Generate(UserModel userModel)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
				new Claim(ClaimTypes.Name, userModel.UserName),
				new Claim(ClaimTypes.Email, userModel.Email),
			};

			DateTime expirationDateTime = DateTime.UtcNow.AddMinutes(configuration.AccessTokenExpirationMinutes);
			SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.AccessTokenSecret));
			SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken jwtToken = new JwtSecurityToken(
				configuration.Issuer,
				configuration.Audience,
				claims,
				DateTime.UtcNow,
				expirationDateTime,
				credentials);

			string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

			return new AccessToken(token, expirationDateTime);
		}
	}
}
