using System.ComponentModel.DataAnnotations;
using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Posts
{
	public class PostCreationModel
	{
		public PostCreationModel()
		{

		}

		public PostCreationModel(Post model)
		{
			Name = model.Name;
			Text = model.Text;
		}

		[Required]
		public string Name { get; set; }

		[Required]
		public string Text { get; set; }

		public Post ToEntity()
		{
			return new Post
			{
				Name = Name,
				Text = Text
			};
		}
	}
}
