using System.ComponentModel.DataAnnotations;
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
			this.Email = dbModel.Email;
			this.Password = dbModel.Password;
		}

		public string Email { get; set; }

		public string Password { get; set; }

		public User ToEntry()
		{
			return new User
			{
				Email = Email,
				Password = Password
			};
		}
	}
}
