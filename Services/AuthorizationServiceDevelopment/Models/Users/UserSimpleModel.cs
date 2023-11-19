using AuthorizationServiceDevelopment.Data.Models;

namespace AuthorizationServiceDevelopment.Models.Users
{
	public class UserSimpleModel
	{
		public UserSimpleModel()
		{

		}

		public UserSimpleModel(User dbModel)
		{
			this.Id = dbModel.Id;
			this.UserName = dbModel.UserName;
		}

		public int Id { get; set; }

		public string UserName { get; set; }
	}
}
