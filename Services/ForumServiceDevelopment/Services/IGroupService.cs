using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;

namespace ForumServiceDevelopment.Services
{
	public interface IGroupService
	{
		List<GroupSimpleModel> GetGroups();

		GroupFullModel GetGroupById(Guid groupId);

		GroupFullModel AddGroup(GroupCreationModel model, Guid authorId);


		List<PostSimpleModel> GetPosts(string text);

		PostModel GetPostById(Guid postId);


		PostModel AddPost(Guid groupId, PostCreationModel model, Guid authorId);


		CommentModel AddComment(Guid postId, CommentCreationModel model, Guid authorId);

		CommentReactionsModel GetCommentReactions(Guid commentId, Guid userId);

		CommentReactionsModel UpdateCommentReaction(Guid commentId, CommentReactionEnum reaction, Guid userId);
	}
}
