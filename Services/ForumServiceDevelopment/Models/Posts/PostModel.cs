using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models.Comments;

namespace ForumServiceDevelopment.Models.Posts
{
	public class PostModel
	{
		public PostModel(Post model)
		{
			Id= model.Id;
			Name = model.Name;
			Text = model.Text;
			AuthorId = model.AuthorId;
			Comments = model.Comments.ConvertAll(c => new CommentModel(c));
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Text { get; set; }

		public Guid AuthorId { get; set; }

		public List<CommentModel> Comments { get; set; }
	}
}
