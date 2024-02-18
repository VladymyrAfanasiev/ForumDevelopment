using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;
using ForumServiceDevelopment.Models.Requests;
using ForumServiceDevelopment.Services;
using InfrastructureServiceDevelopment.Authentication;
using InfrastructureServiceDevelopment.Extensions;
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
		[RoleRequirementFilter(UserRole.Admin)]
		[HttpGet("request")]
		public IActionResult GetRequests()
		{
			List<RequestGroupModel> models = this.groupService.GetRequests();

			return Ok(models);
		}

		[Authorize]
		[HttpGet("userrequests")]
		public IActionResult GetUserRequests()
		{
			UserInfo userInfo = User.GetAuthorizedUserInfo();
			List<RequestGroupModel> models = this.groupService.GetRequests(userInfo.Id);

			return Ok(models);
		}

		[Authorize]
		[HttpPut("request")]
		public IActionResult RequestAddGroup(RequestAddGroupModel requestAddGroupModel)
		{
			UserInfo userInfo = User.GetAuthorizedUserInfo();
			RequestGroupModel model = this.groupService.AddGroupRequest(requestAddGroupModel, userInfo.Id);
			if (model == null)
			{
				return BadRequest();
			}

			if (userInfo.Role == UserRole.Admin)
			{
				this.groupService.ChangeRequestState(model.Id, RequestStatusEnum.Approved);
			}

			return Ok(model);
		}

		[Authorize]
		[RoleRequirementFilter(UserRole.Admin)]
		[HttpPost("request/{requestId:guid}")]
		public IActionResult ProcessRequest(Guid requestId, ProcessRequestModel processRequestModel)
		{
			if (!Enum.TryParse(processRequestModel.newStatus, out RequestStatusEnum newRequestStatusEnum))
			{
				return BadRequest();
			}

			RequestGroupModel model = this.groupService.ChangeRequestState(requestId, newRequestStatusEnum);
			if (model == null)
			{
				return BadRequest();
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
			PostModel model = this.groupService.AddPost(groupId, creationModel, User.GetAuthorizedUserInfo().Id);
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
			CommentModel model = this.groupService.AddComment(postId, creationModel, User.GetAuthorizedUserInfo().Id);
			if (model == null)
			{
				BadRequest();
			}

			return Ok(model);
		}

		[HttpGet("{groupId:guid}/post/{postId:guid}/comment/{commentId:guid}/reaction")]
		public IActionResult GetCommentReactions(Guid groupId, Guid postId, Guid commentId)
		{
			CommentReactionsModel models = this.groupService.GetCommentReactions(commentId, User.GetAuthorizedUserInfo().Id);
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

			CommentReactionsModel model = this.groupService.UpdateCommentReaction(commentId, reactionEnum, User.GetAuthorizedUserInfo().Id);
			if (model == null)
			{
				return BadRequest();
			}

			return Ok(model);
		}
	}
}
