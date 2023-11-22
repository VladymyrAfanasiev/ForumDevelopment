using System.ComponentModel.DataAnnotations;
using AuthorizationServiceDevelopment.Data.Models;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserCreationModel
	{
		public UserCreationModel()
		{
			
		}

		public UserCreationModel(User dbModel)
		{

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
				Email = Email
			};
		}
	}
}
