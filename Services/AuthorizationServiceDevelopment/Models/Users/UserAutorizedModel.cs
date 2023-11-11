using AuthorizationServiceDevelopment.Data.Models;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserAutorizedModel
	{
		public UserAutorizedModel()
		{

		}

		public UserAutorizedModel(string userName, string token, DateTime expirationTime)
		{
			UserName = userName;
			Token = token;
			ExpirationTime = expirationTime;
		}

		public string UserName { get; }

		public string Token { get; set; }
		public DateTime ExpirationTime { get; }

		public User ToEntry()
		{
			return new User
			{
				UserName = UserName
			};
		}
	}
}
