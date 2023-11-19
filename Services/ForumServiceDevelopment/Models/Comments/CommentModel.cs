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
			CreatedOn = model.CreatedOn;
			AuthorId = model.AuthorId;
		}

		public int Id { get; set; }

		public string Text { get; set; }

		public int AuthorId { get; set; }

		public DateTime CreatedOn { get; set; }

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
