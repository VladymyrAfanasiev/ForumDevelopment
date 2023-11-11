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

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[EmailAddress]
		[Required]
		public string Email { get; set; }

		public RoleNames Role { get; set;}

	}
}
