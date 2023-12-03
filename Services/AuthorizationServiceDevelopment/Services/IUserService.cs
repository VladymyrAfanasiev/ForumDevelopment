using AuthorizationServiceDevelopment.Models.Users;

namespace AuthorizationServiceDevelopment.Services
{
	public interface IUserService
	{
		UserModel CreateUser(UserCreationModel creationModel, string passwordHash, string salt);
		UserModel GetUser(UserAuthenticationModel authorizationModel);
		UserModel GetUserById(Guid id);
		bool CheckUserExistance(UserCreationModel creationModel);
		string GetUserSalt(string email);
		string GetUserPasswordHash(UserAuthenticationModel authorizationModel);
	}
}
