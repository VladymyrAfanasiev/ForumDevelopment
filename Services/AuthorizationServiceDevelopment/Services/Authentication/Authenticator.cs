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

		public UserAutorizedModel Authenticate(UserModel userModel)
		{
			AccessToken token = this.accessTokenGenerator.Generate(userModel);

			return new UserAutorizedModel(userModel, token);
		}

	}
}
