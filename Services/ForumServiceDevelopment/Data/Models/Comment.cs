namespace ForumServiceDevelopment.Data.Models
{
	public class Comment
	{
		public Guid Id { get; set; }

		public string Text { get; set; }

		public Guid AuthorId { get; set; }

		public DateTime CreatedOn { get; set; }

		public List<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();


		public Post? Post { get; set; }

		public Guid? PostId { get; set; }
	}
}
