namespace ForumServiceDevelopment.Data.Models
{
	public class CommentReaction
	{
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		public int ReactionId { get; set; }


		public Comment? Comment { get; set; }

		public Guid? CommentId { get; set; }
	}
}
