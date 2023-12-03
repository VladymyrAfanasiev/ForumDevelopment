using ForumServiceDevelopment.Data.Models;

namespace ForumServiceDevelopment.Models.Comments
{
	public class CommentReactionsModel
	{
		public CommentReactionsModel(List<CommentReaction> commentReactions, Guid userId)
		{
			this.LikeCount = commentReactions.Count(r => r.ReactionId == (int)CommentReactionEnum.Like);
			this.DislikeCount = commentReactions.Count(r => r.ReactionId == (int)CommentReactionEnum.Dislike);
			this.UserReactionId = commentReactions.FirstOrDefault(r => r.UserId == userId)?.ReactionId ?? (int)CommentReactionEnum.NoReaction;
		}

		public int LikeCount { get; set; }

		public int DislikeCount { get; set; }

		public int UserReactionId { get; set; }
	}
}
