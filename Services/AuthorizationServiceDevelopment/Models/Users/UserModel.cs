using System.ComponentModel.DataAnnotations;
using AuthorizationServiceDevelopment.Data.Models;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserModel
	{
		public UserModel()
		{

		}

		public UserModel(User dbModel)
		{
			this.Id = dbModel.Id;
			this.UserName = dbModel.UserName;
			this.Email = dbModel.Email;
		}

		public int Id { get; set; }

		public string UserName { get; set; }


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
