namespace AuthorizationServiceDevelopment.Models.Tokens
{
	public class AccessToken
	{
		public AccessToken(string token, DateTime expirationTime)
		{
			Token = token;
			ExpirationTime = expirationTime;
		}

		public string Token { get; }

		public DateTime ExpirationTime { get; }
	}
}
