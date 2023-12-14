using System.ComponentModel.DataAnnotations;

namespace AuthorizationServiceDevelopment.Data.Models
{
	public enum UserRole
	{
		User = 0,
		Admin = 1
	}

	public class User
	{
		public Guid Id { get; set; }

		public string UserName { get; set; }

		public string PasswordHash { get; set; }

		public string Salt { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public DateTime JoinedOn { get; set; }

		public UserRole Role { get; set;}

	}
}
