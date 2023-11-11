using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models
{
	public class GroupCreationModel
	{
		public GroupCreationModel()
		{

		}

		public GroupCreationModel(Group model)
		{
			Name = model.Name;
			Description = model.Description;
		}

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public Group ToEntity()
		{
			return new Group
			{
				Name = Name,
				Description = Description
			};
		}
	}
}
