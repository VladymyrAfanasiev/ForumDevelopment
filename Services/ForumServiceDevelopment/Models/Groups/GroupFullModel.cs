using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models.Posts;

namespace ForumServiceDevelopment.Models
{
	public class GroupFullModel
	{
		public GroupFullModel(Group model)
		{
			Id= model.Id;
			Name = model.Name;
			Description = model.Description;
			Posts = model.Posts.ConvertAll(p => new PostSimpleModel(p));
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public List<PostSimpleModel> Posts { get; set; }

		public Group ToEntity()
		{
			return new Group
			{
				Id = Id,
				Name = Name,
				Description = Description,
				Posts = Posts.ConvertAll(p => p.ToEntity())
			};
		}
	}
}
