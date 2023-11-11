using ForumServiceDevelopment.Data;
using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace ForumServiceDevelopment.Services
{
	public class GroupService : IGroupService
	{
		private readonly ForumDatabaseContext databaseContext;

		public GroupService(ForumDatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public List<GroupSimpleModel> GetGroups()
		{
			return this.databaseContext.Groups.Select(g => new GroupSimpleModel(g)).ToList();
		}

		public GroupFullModel GetGroupById(int groupId)
		{
			Group group = this.databaseContext.Groups
				.Include(g => g.Posts)
				.FirstOrDefault(gi => gi.Id == groupId);

			if (group == null)
			{
				return null;
			}

			return new GroupFullModel(group);
		}

		public GroupFullModel AddGroup(GroupCreationModel model)
		{
			Group newGroup = model.ToEntity();

			this.databaseContext.Groups.Add(newGroup);
			this.databaseContext.SaveChanges();

			return new GroupFullModel(newGroup);
		}


		public PostFullModel GetPostById(int postId)
		{
			Post post = this.databaseContext.Posts
				.Include(g => g.Comments)
				.FirstOrDefault(gi => gi.Id == postId);
			if (post == null)
			{
				return null;
			}

			return new PostFullModel(post);
		}

		public PostFullModel AddPost(int groupId, PostCreationModel model)
		{
			Group group = this.databaseContext.Groups.FirstOrDefault(gi => gi.Id == groupId);
			if (group == null)
			{
				return null;
			}

			Post newPost = model.ToEntity();
			newPost.GroupId = group.Id;

			this.databaseContext.Posts.Add(newPost);
			this.databaseContext.SaveChanges();

			return new PostFullModel(newPost);
		}


		public CommentModel AddComment(int postId, CommentCreationModel model)
		{
			Post post = this.databaseContext.Posts.FirstOrDefault(gi => gi.Id == postId);
			if (post == null)
			{
				return null;
			}

			Comment newComment = model.ToEntity();
			newComment.PostId = post.Id;

			this.databaseContext.Comments.Add(newComment);
			this.databaseContext.SaveChanges();

			return new CommentModel(newComment);
		}
	}
}
