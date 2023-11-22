using AuthorizationServiceDevelopment.Data;
using AuthorizationServiceDevelopment.Data.Models;
using AuthorizationServiceDevelopment.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationServiceDevelopment.Services
{
	public class UserService : IUserService
	{
		private readonly AuthorizationDbContext dbContext;

		public UserService(AuthorizationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public UserModel CreateUser(UserCreationModel creationModel, string passwordHash, string salt)
		{
			if (CheckUserExistance(creationModel))
			{
				return null;
			}

			User dbCreationModel = creationModel.ToEntry();
			dbCreationModel.PasswordHash = passwordHash;
			dbCreationModel.Salt = salt;
			dbCreationModel.JoinedOn = DateTime.Now;
			dbContext.Users.Add(dbCreationModel);
			dbContext.SaveChanges();

			return new UserModel(dbCreationModel);
		}

		public UserModel GetUser(UserAuthenticationModel authorizationModel)
		{
			User dbModel = dbContext.Users
				.FirstOrDefault(u => u.Email == authorizationModel.Email &&
									 u.PasswordHash == authorizationModel.PasswordHash);

			return dbModel == null ? null : new UserModel(dbModel);
		}

		public UserModel GetUserById(int id)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Id == id);

			return dbModel == null ? null : new UserModel(dbModel);
		}

		public UserModel GetUserByEmail(string email)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Email == email);

			return dbModel == null ? null : new UserModel(dbModel);
		}

		public bool CheckUserExistance(UserCreationModel creationModel)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.UserName == creationModel.UserName ||
				u.Email == creationModel.Email);

			return dbModel != null;
		}

		public string GetUserSalt(string email)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Email == email);

			return dbModel != null ? dbModel.Salt : string.Empty;
		}

		public string GetUserPasswordHash(UserAuthenticationModel authorizationModel)
		{
			User dbModel = dbContext.Users.FirstOrDefault(u => u.Email == authorizationModel.Email);

			return dbModel != null ? dbModel.PasswordHash : string.Empty;
		}
	}
}
