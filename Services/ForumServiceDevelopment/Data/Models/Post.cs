namespace ForumServiceDevelopment.Data.Models
{
	public class Post
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Text { get; set; }

		public Guid AuthorId { get; set; }

		public DateTime CreatedOn { get; set; }

		public List<Comment> Comments { get; set; } = new List<Comment>();


		public Group? Group { get; set; }

		public Guid? GroupId { get; set; }
	}
}
