namespace ForumServiceDevelopment.Data.Models
{
	public class Post
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Text { get; set; }

		public List<Comment> Comments { get; set; } = new List<Comment>();


		public Group? Group { get; set; }

		public int? GroupId { get; set; }
	}
}
