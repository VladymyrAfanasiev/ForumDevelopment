using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models
{
	public class GroupSimpleModel
	{
		public GroupSimpleModel(Group model)
		{
			Id = model.Id;
			Name = model.Name;
			Description = model.Description;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public Group ToEntity()
		{
			return new Group
			{
				Id = Id,
				Name = Name,
				Description = Description
			};
		}
	}
}
