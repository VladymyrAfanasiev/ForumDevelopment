using AuthorizationServiceDevelopment.Models.Tokens;
using AuthorizationServiceDevelopment.Models.Users;

namespace AuthorizationServiceDevelopment.Services.Authentication
{
	public class Authenticator
	{
		private readonly AccessTokenGenerator accessTokenGenerator;

		public Authenticator(AccessTokenGenerator accessTokenGenerator)
		{
			this.accessTokenGenerator = accessTokenGenerator;
		}

		public UserAutorizedModel Authenticate(UserAuthenticationModel authorizationModel)
		{
			AccessToken token = this.accessTokenGenerator.Generate(authorizationModel);

			return new UserAutorizedModel(authorizationModel.UserName, token.Token, token.ExpirationTime);
		}
	}
}
