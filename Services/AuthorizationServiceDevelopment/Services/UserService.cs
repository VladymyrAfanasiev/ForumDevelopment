using AuthorizationServiceDevelopment.Data;
using AuthorizationServiceDevelopment.Data.Models;
using AuthorizationServiceDevelopment.Models.Users;

namespace AuthorizationServiceDevelopment.Services
{
	public class UserService : IUserService
	{
		private readonly AuthorizationDbContext dbContext;

		public UserService(AuthorizationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public UserModel CreateUser(UserCreationModel creationModel)
		{
			User dbCreationModel = creationModel.ToEntry();

			dbContext.Users.Add(dbCreationModel);
			dbContext.SaveChanges();

			return new UserModel(dbCreationModel);
		}

		public UserModel GetUser(UserAuthenticationModel authorizationModel)
		{
			User dbModel = dbContext.Users.FirstOrDefault(
				u => u.UserName == authorizationModel.UserName &&
				u.Email == authorizationModel.Email &&
				u.Password == authorizationModel.Password);

			return dbModel == null ? null : new UserModel(dbModel);
		}
	}
}
