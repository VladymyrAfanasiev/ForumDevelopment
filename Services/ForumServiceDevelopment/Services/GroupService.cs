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

		public GroupFullModel GetGroupById(Guid groupId)
		{
			Group group = this.databaseContext.Groups.Include(g => g.Posts).ThenInclude(p => p.Comments).FirstOrDefault(gi => gi.Id == groupId);
			if (group == null)
			{
				return null;
			}

			return new GroupFullModel(group);
		}

		public List<PostSimpleModel> GetPosts(string text)
		{
			return this.databaseContext.Posts
				.Where(p => p.Name.Contains(text) || p.Text.Contains(text))
				.Include(p => p.Comments)
				.Select(g => new PostSimpleModel(g))
				.ToList();
		}

		public PostModel GetPostById(Guid postId)
		{
			Post post = this.databaseContext.Posts
				.Include(p => p.Comments)
				.FirstOrDefault(gi => gi.Id == postId);
			if (post == null)
			{
				return null;
			}

			return new PostModel(post);
		}

		public PostModel AddPost(Guid groupId, PostCreationModel model, Guid authorId)
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

			return new PostModel(newPost);
		}


		public CommentModel AddComment(Guid postId, CommentCreationModel model, Guid authorId)
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

		public CommentReactionsModel GetCommentReactions(Guid commentId, Guid userId)
		{
			Comment comment = this.databaseContext.Comments.Include(c => c.CommentReactions).FirstOrDefault(c => c.Id == commentId);
			if (comment == null)
			{
				return null;
			}

			return new CommentReactionsModel(comment.CommentReactions, userId);
		}

		public CommentReactionsModel UpdateCommentReaction(Guid commentId, CommentReactionEnum newReaction, Guid userId)
		{
			Comment comment = this.databaseContext.Comments.Include(c => c.CommentReactions).FirstOrDefault(c => c.Id == commentId);
			if (comment == null)
			{
				return null;
			}

			CommentReaction reaction = comment.CommentReactions.FirstOrDefault(r => r.UserId == userId);
			if (reaction == null)
			{
				reaction = new CommentReaction();
				reaction.UserId = userId;
				reaction.CommentId = commentId;
				reaction.ReactionId = (int)newReaction;

				comment.CommentReactions.Add(reaction);

				this.databaseContext.CommentReactions.Add(reaction);
			}
			else if (reaction.ReactionId == (int)newReaction)
			{
				reaction.ReactionId = (int)CommentReactionEnum.NoReaction;
			}
			else
			{
				reaction.ReactionId = (int)newReaction;
			}

			this.databaseContext.SaveChanges();

			return new CommentReactionsModel(comment.CommentReactions, userId);
		}
	}
}
