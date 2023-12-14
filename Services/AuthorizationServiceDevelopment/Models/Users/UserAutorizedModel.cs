using System.ComponentModel.DataAnnotations;
using AuthorizationServiceDevelopment.Data.Models;
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
			Id = userModel.Id;
			UserName = userModel.Name;
			Email = userModel.Email;
			Role = userModel.Role;
			JoinedOn = userModel.JoinedOn;
			Token = token.Token;
			ExpirationTime = token.ExpirationTime;
		}

		public Guid Id { get; }

		public string UserName { get; }

		[EmailAddress]
		public string Email { get; }

		public UserRole Role { get; }

		public DateTime JoinedOn { get; }

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
