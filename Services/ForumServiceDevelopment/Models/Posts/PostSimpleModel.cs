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
			AuthorId = model.AuthorId;
			CommentsCount = model.Comments.Count();
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Text { get; set; }

		public Guid AuthorId { get; set; }

		public int CommentsCount { get; set; }
	}
}
