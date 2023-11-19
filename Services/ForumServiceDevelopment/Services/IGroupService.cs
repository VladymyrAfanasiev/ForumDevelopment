using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;

namespace ForumServiceDevelopment.Services
{
	public interface IGroupService
	{
		List<GroupSimpleModel> GetGroups();

		GroupFullModel GetGroupById(int groupId);

		PostFullModel GetPostById(int postId);


		GroupFullModel AddGroup(GroupCreationModel model, int authorId);

		PostFullModel AddPost(int groupId, PostCreationModel model, int authorId);

		CommentModel AddComment(int postId, CommentCreationModel model, int authorId);
	}
}
