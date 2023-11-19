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
			if (CheckUserExistance(creationModel))
			{
				return null;
			}

			User dbCreationModel = creationModel.ToEntry();

			dbContext.Users.Add(dbCreationModel);
			dbContext.SaveChanges();

			return new UserModel(dbCreationModel);
		}

		public UserModel GetUser(UserAuthenticationModel authorizationModel)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Email == authorizationModel.Email);

			return dbModel == null ? null : new UserModel(dbModel);
		}

		public UserModel GetUser(int id)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Id == id);

			return dbModel == null ? null : new UserModel(dbModel);
		}

		public bool CheckUserExistance(UserCreationModel creationModel)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u =>
				u.UserName == creationModel.UserName ||
				u.Email == creationModel.Email);

			return dbModel != null;
		}
	}
}
