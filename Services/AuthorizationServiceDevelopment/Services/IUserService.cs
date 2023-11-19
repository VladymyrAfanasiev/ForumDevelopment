using AuthorizationServiceDevelopment.Models.Users;

namespace AuthorizationServiceDevelopment.Services
{
	public interface IUserService
	{
		UserModel CreateUser(UserCreationModel creationModel);
		UserModel GetUser(UserAuthenticationModel authorizationModel);
		UserModel GetUserById(int id);
		bool CheckUserExistance(UserCreationModel creationModel);
	}
}
