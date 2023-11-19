using System.ComponentModel.DataAnnotations;

namespace AuthorizationServiceDevelopment.Data.Models
{
	public enum RoleNames
	{
		User,
		Admin
	}

	public class User
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public RoleNames Role { get; set;}

	}
}
