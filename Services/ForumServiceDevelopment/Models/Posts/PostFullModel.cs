using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models.Comments;

namespace ForumServiceDevelopment.Models.Posts
{
	public class PostFullModel
	{
		public PostFullModel(Post model)
		{
			Id= model.Id;
			Name = model.Name;
			Text = model.Text;
			Comments = model.Comments.ConvertAll(c => new CommentModel(c));
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Text { get; set; }

		public List<CommentModel> Comments { get; set; }

		public Post ToEntity()
		{
			return new Post
			{
				Id = Id,
				Name = Name,
				Text = Text,
				Comments = Comments.ConvertAll(x => x.ToEntity())
			};
		}
	}
}
