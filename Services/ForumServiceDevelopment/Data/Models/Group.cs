namespace ForumServiceDevelopment.Data.Models
{
	public class Group
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public Guid AuthorId { get; set; }

		public DateTime CreatedOn { get; set; }

		public List<Post> Posts { get; set; } = new List<Post>();
	}
}
