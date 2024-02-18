using System.ComponentModel.DataAnnotations;
using AuthorizationServiceDevelopment.Data.Models;
using InfrastructureServiceDevelopment.Authentication;

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
			this.Name = dbModel.UserName;
			this.Email = dbModel.Email;
			this.JoinedOn = dbModel.JoinedOn;
			this.Role = dbModel.Role;
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public DateTime JoinedOn { get; set; }

		public UserRole Role { get; set; }

		public User ToEntry()
		{
			return new User
			{
				UserName = Name,
				Email = Email
			};
		}
	}
}
