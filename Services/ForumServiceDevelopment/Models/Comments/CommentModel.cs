using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Comments
{
	public class CommentModel
	{
		public CommentModel()
		{

		}

		public CommentModel(Comment model)
		{
			Id = model.Id;
			Text = model.Text;
		}

		public int Id { get; set; }

		public string Text { get; set; }

		public Comment ToEntity()
		{
			return new Comment
			{
				Id = Id,
				Text = Text
			};
		}
	}
}
