namespace ForumServiceDevelopment.Data.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public int AuthorId { get; set; }

		public DateTime CreatedOn { get; set; }


		public Post? Post { get; set; }

		public int? PostId { get; set; }
	}
}
