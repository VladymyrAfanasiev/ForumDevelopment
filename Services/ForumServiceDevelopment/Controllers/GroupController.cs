using System.Security.Claims;
using ForumServiceDevelopment.Models;
using ForumServiceDevelopment.Models.Comments;
using ForumServiceDevelopment.Models.Posts;
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

		[HttpGet]
		public IActionResult GetGroups()
		{
			List<GroupSimpleModel> models = this.groupService.GetGroups();

			return Ok(models);
		}

		[HttpGet("{groupId:int}")]
		public IActionResult GetGroupById(int groupId)
		{
			GroupFullModel model = this.groupService.GetGroupById(groupId);
			if (model == null)
			{
				return NotFound("Group not found");
			}

			return Ok(model);
		}

		[HttpGet("{groupId:int}/post/{postId:int}")]
		public IActionResult GetPostById(int groupId, int postId)
		{
			PostFullModel model = this.groupService.GetPostById(postId);
			if (model == null)
			{
				return NotFound("Post not found");
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut]
		public IActionResult AddGroup(GroupCreationModel creationModel)
		{
			GroupFullModel model = this.groupService.AddGroup(creationModel, GetAuthorizedUserId());
			if (model == null)
			{
				return BadRequest("Group already exists");
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:int}/post")]
		public IActionResult AddPost(int groupId, PostCreationModel creationModel)
		{
			PostFullModel model = this.groupService.AddPost(groupId, creationModel, GetAuthorizedUserId());
			if (model == null)
			{
				return BadRequest("Failed to add post");
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:int}/post/{postId:int}/comment")]
		public IActionResult AddComment(int groupId, int postId, CommentCreationModel creationModel)
		{
			CommentModel model = this.groupService.AddComment(postId, creationModel, GetAuthorizedUserId());
			if (model == null)
			{
				BadRequest();
			}

			return Ok(model);
		}

		private int GetAuthorizedUserId()
		{
			string authorId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
			if (!string.IsNullOrEmpty(authorId) && int.TryParse(authorId, out int id))
			{
				return id;
			}

			return -1000;
		}
	}
}
