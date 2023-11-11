using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;

namespace ForumServiceDevelopment.Services
{
	public interface IGroupService
	{
		List<GroupSimpleModel> GetGroups();
		GroupFullModel GetGroupById(int groupId);
		GroupFullModel AddGroup(GroupCreationModel model);


		PostFullModel GetPostById(int postId);
		PostFullModel AddPost(int groupId, PostCreationModel model);


		CommentModel AddComment(int postId, CommentCreationModel model);
	}
}
