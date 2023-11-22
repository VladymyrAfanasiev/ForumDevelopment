namespace AuthorizationServiceDevelopment.Managers
{
	public interface IPasswordManager
	{
		string GeneratePasswordHash(string password, out string salt);
	}
}
