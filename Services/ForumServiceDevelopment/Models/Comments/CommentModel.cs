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
			AuthorId = model.AuthorId;
			CreatedOn = model.CreatedOn;
		}

		public Guid Id { get; set; }

		public string Text { get; set; }

		public Guid AuthorId { get; set; }

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
