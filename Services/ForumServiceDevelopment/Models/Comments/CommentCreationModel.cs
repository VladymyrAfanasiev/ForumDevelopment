using System.ComponentModel.DataAnnotations;
using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Comments
{
	public class CommentCreationModel
	{
		public CommentCreationModel()
		{

		}

		public CommentCreationModel(Comment model)
		{
			Text = model.Text;
		}


		[Required]
		public string Text { get; set; }

		public Comment ToEntity()
		{
			return new Comment
			{
				Text = Text
			};
		}
	}
}
