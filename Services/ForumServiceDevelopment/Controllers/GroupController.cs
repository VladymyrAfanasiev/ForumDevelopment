using System.Security.Claims;
using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;
using ForumServiceDevelopment.Models.Requests;
using ForumServiceDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumServiceDevelopment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly IGroupService groupService;

		public GroupController(IGroupService groupService)
		{
			this.groupService = groupService;
		}

		[Authorize]
		[HttpGet("request")]
		public IActionResult GetRequests()
		{
			List<RequestGroupModel> models;
			UserInfo userInfo = GetAuthorizedUserInfo();
			if (userInfo.Role == UserRole.Admin)
			{
				models = this.groupService.GetRequests();
			}
			else if (userInfo.Role == UserRole.User)
			{
				models = this.groupService.GetRequests(userInfo.Id);
			}
			else
			{
				return BadRequest();
			}

			return Ok(models);
		}

		[Authorize]
		[HttpPut("request")]
		public IActionResult RequestAddGroup(RequestAddGroupModel requestAddGroupModel)
		{
			UserInfo userInfo = GetAuthorizedUserInfo();
			RequestGroupModel model = this.groupService.AddGroupRequest(requestAddGroupModel, userInfo.Id);
			if (model == null)
			{
				return BadRequest();
			}

			if (userInfo.Role == UserRole.Admin)
			{
				this.groupService.ApproveRequest(model.Id);
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPost("request/{requestId:guid}")]
		public IActionResult AddGroup(Guid requestId)
		{
			UserInfo userInfo = GetAuthorizedUserInfo();
			if (userInfo.Role != UserRole.Admin)
			{
				return BadRequest("You do not have enough permissions for the operation");
			}

			RequestGroupModel model = this.groupService.ApproveRequest(requestId);
			if (model == null)
			{
				return BadRequest("Group with the same name already exists");
			}

			return Ok(model);
		}

		[HttpGet]
		public IActionResult GetGroups()
		{
			List<GroupSimpleModel> models = this.groupService.GetGroups();

			return Ok(models);
		}

		[HttpGet("{groupId:guid}")]
		public IActionResult GetGroupById(Guid groupId)
		{
			GroupFullModel model = this.groupService.GetGroupById(groupId);
			if (model == null)
			{
				return NotFound("Group not found");
			}

			return Ok(model);
		}

		[HttpGet("{groupId:guid}/post/{postId:guid}")]
		public IActionResult GetPostById(Guid groupId, Guid postId)
		{
			PostModel model = this.groupService.GetPostById(postId);
			if (model == null)
			{
				return NotFound("Post not found");
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:guid}/post")]
		public IActionResult AddPost(Guid groupId, PostCreationModel creationModel)
		{
			PostModel model = this.groupService.AddPost(groupId, creationModel, GetAuthorizedUserInfo().Id);
			if (model == null)
			{
				return BadRequest("Failed to add post");
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:guid}/post/{postId:guid}/comment")]
		public IActionResult AddComment(Guid groupId, Guid postId, CommentCreationModel creationModel)
		{
			CommentModel model = this.groupService.AddComment(postId, creationModel, GetAuthorizedUserInfo().Id);
			if (model == null)
			{
				BadRequest();
			}

			return Ok(model);
		}

		[HttpGet("{groupId:guid}/post/{postId:guid}/comment/{commentId:guid}/reaction")]
		public IActionResult GetCommentReactions(Guid groupId, Guid postId, Guid commentId)
		{
			CommentReactionsModel models = this.groupService.GetCommentReactions(commentId, GetAuthorizedUserInfo().Id);
			if (models == null)
			{
				return NotFound("Comment not found");
			}

			return Ok(models);
		}

		[Authorize]
		[HttpPost("{groupId:guid}/post/{postId:guid}/comment/{commentId:guid}/reaction")]
		public IActionResult UpdateCommentReaction(Guid groupId, Guid postId, Guid commentId, [FromBody]int reaction)
		{
			CommentReactionEnum reactionEnum;
			if (Enum.IsDefined(typeof(CommentReactionEnum), reaction))
			{
				reactionEnum = (CommentReactionEnum)reaction;
			}
			else
			{
				return BadRequest();
			}

			CommentReactionsModel model = this.groupService.UpdateCommentReaction(commentId, reactionEnum, GetAuthorizedUserInfo().Id);
			if (model == null)
			{
				return BadRequest();
			}

			return Ok(model);
		}

		private UserInfo GetAuthorizedUserInfo()
		{
			string claimId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			string claimRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
			if (!string.IsNullOrEmpty(claimId) && Guid.TryParse(claimId, out Guid id) &&
				!string.IsNullOrEmpty(claimRole) && UserRole.TryParse(claimRole, out UserRole role))
			{
				return new UserInfo(id, role);
			}

			return new UserInfo();
		}

		private enum UserRole
		{
			Undefined = -1,
			User = 0,
			Admin = 1
		}

		private class UserInfo
		{
			public UserInfo()
			{
				Id = Guid.Empty;
				Role = UserRole.Undefined;
			}

			public UserInfo(Guid id, UserRole role)
			{
				Id = id;
				Role = role;
			}

			public Guid Id { get; }

			public UserRole Role { get; }
		}
	}
}
