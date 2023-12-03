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
			AuthorId = model.AuthorId;
			Posts = model.Posts.ConvertAll(p => new PostSimpleModel(p));
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AuthorId { get; set; }

		public List<PostSimpleModel> Posts { get; set; }
	}
}
