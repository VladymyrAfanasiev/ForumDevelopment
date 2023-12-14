using System.ComponentModel.DataAnnotations;
using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Requests
{
	public class RequestGroupModel
	{
		public RequestGroupModel()
		{

		}

		public RequestGroupModel(GroupRequest model)
		{
			Id = model.Id;
			Name = model.Name;
			Description = model.Description;
			AuthorId = model.AuthorId;
			Status = model.Status;
		}

		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AuthorId { get; set; }

		public RequestStatusEnum Status { get; set; }

		public Group ToGroupEntity()
		{
			return new Group
			{
				Name = Name,
				Description = Description,
				CreatedOn = DateTime.Now,
				AuthorId = AuthorId
			};
		}
	}
}
