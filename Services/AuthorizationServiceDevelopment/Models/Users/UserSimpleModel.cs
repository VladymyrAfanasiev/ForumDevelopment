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
			this.Name = dbModel.UserName;
		}

		public Guid Id { get; set; }

		public string Name { get; set; }
	}
}
