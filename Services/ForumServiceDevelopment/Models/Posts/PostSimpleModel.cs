using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Posts
{
	public class PostSimpleModel
	{
		public PostSimpleModel(Post model)
		{
			Id = model.Id;
			Name = model.Name;
			Text= model.Text;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Text { get; set; }

		public string Author { get; set; }

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
