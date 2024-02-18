using System.ComponentModel.DataAnnotations;
using InfrastructureServiceDevelopment.Authentication;

namespace AuthorizationServiceDevelopment.Data.Models
{
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
