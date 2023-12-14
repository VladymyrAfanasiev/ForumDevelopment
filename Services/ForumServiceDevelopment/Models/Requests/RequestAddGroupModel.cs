using System.ComponentModel.DataAnnotations;
using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Requests
{
	public class RequestAddGroupModel
	{
		public RequestAddGroupModel()
		{

		}

		public RequestAddGroupModel(Group model)
		{
			Name = model.Name;
			Description = model.Description;
		}

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public GroupRequest ToGroupRequestEntity()
		{
			return new GroupRequest
			{
				Name = Name,
				Description = Description,
				Status = RequestStatusEnum.New
			};
		}

		public Group ToGroupEntity()
		{
			return new Group
			{
				Name = Name,
				Description = Description,
				CreatedOn = DateTime.Now
			};
		}
	}
}
