using AuthorizationServiceDevelopment.Models.Tokens;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserAutorizedModel
	{
		public UserAutorizedModel()
		{

		}

		public UserAutorizedModel(UserModel userModel, AccessToken token)
		{
			this.User = userModel;
			this.Token = token;
		}

		public UserModel User { get; }

		public AccessToken Token { get; }
	}
}
