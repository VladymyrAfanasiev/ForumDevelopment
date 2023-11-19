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
			return this.databaseContext.Groups
				.Include(g => g.Posts)
				.ThenInclude(p => p.Comments)
				.Select(g => new GroupSimpleModel(g))
				.ToList();
		}

		public GroupFullModel GetGroupById(int groupId)
		{
			Group group = this.databaseContext.Groups.Include(g => g.Posts).FirstOrDefault(gi => gi.Id == groupId);
			if (group == null)
			{
				return null;
			}

			return new GroupFullModel(group);
		}

		public PostFullModel GetPostById(int postId)
		{
			Post post = this.databaseContext.Posts.Include(g => g.Comments).FirstOrDefault(gi => gi.Id == postId);
			if (post == null)
			{
				return null;
			}

			return new PostFullModel(post);
		}


		public GroupFullModel AddGroup(GroupCreationModel model, int authorId)
		{
			Group existedGroup = this.databaseContext.Groups.FirstOrDefault(g => g.Name == model.Name);
			if (existedGroup != null)
			{
				return null;
			}

			Group newGroup = model.ToEntity();
			newGroup.AuthorId = authorId;

			this.databaseContext.Groups.Add(newGroup);
			this.databaseContext.SaveChanges();

			return new GroupFullModel(newGroup);
		}

		public PostFullModel AddPost(int groupId, PostCreationModel model, int authorId)
		{
			Group group = this.databaseContext.Groups.FirstOrDefault(gi => gi.Id == groupId);
			if (group == null)
			{
				return null;
			}

			Post newPost = model.ToEntity();
			newPost.GroupId = group.Id;
			newPost.AuthorId = authorId;

			this.databaseContext.Posts.Add(newPost);
			this.databaseContext.SaveChanges();

			return new PostFullModel(newPost);
		}

		public CommentModel AddComment(int postId, CommentCreationModel model, int authorId)
		{
			Post post = this.databaseContext.Posts.FirstOrDefault(gi => gi.Id == postId);
			if (post == null)
			{
				return null;
			}

			Comment newComment = model.ToEntity();
			newComment.PostId = post.Id;
			newComment.AuthorId = authorId;

			this.databaseContext.Comments.Add(newComment);
			this.databaseContext.SaveChanges();

			return new CommentModel(newComment);
		}
	}
}
