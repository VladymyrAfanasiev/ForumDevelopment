using AuthorizationServiceDevelopment.Data.Models;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserAuthenticationModel
	{
		public UserAuthenticationModel()
		{

		}

		public UserAuthenticationModel(User dbModel)
		{

		}

		public string Email { get; set; }

		public string PasswordHash { get; set; }
	}
}
