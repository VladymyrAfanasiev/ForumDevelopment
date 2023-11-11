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
			this.UserName = dbModel.UserName;
			this.Password = dbModel.Password;
			this.Email = dbModel.Email;
		}

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public User ToEntry()
		{
			return new User
			{
				UserName = UserName,
				Password = Password,
				Email = Email
			};
		}
	}
}
