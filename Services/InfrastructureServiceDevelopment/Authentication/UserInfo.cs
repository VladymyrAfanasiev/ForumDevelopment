namespace InfrastructureServiceDevelopment.Authentication
{
	public class UserInfo
	{
		public UserInfo()
		{
			Id = Guid.Empty;
			Role = UserRole.Undefined;
		}

		public UserInfo(Guid id, UserRole role)
		{
			Id = id;
			Role = role;
		}

		public Guid Id { get; }

		public UserRole Role { get; }

		public bool IsAdmin { get { return Role == UserRole.Admin; } }
	}
}
