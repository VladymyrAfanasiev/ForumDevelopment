using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Posts
{
	public class PostSimpleModel
	{
		public PostSimpleModel(Post model)
		{
			Id = model.Id;
			Name = model.Name;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public Post ToEntity()
		{
			return new Post
			{
				Id = Id,
				Name = Name
			};
		}
	}
}
