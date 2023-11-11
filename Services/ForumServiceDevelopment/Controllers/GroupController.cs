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
				BadRequest();
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut]
		public IActionResult AddGroup(GroupCreationModel creationModel)
		{
			GroupFullModel model = this.groupService.AddGroup(creationModel);

			return Ok(model);
		}

		[HttpGet("{groupId:int}/post/{postId:int}")]
		public IActionResult GetPostById(int groupId, int postId)
		{
			PostFullModel model = this.groupService.GetPostById(postId);
			if (model == null)
			{
				BadRequest();
			}

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:int}/post")]
		public IActionResult AddPost(int groupId, PostCreationModel creationModel)
		{
			PostFullModel model = this.groupService.AddPost(groupId, creationModel);

			return Ok(model);
		}

		[Authorize]
		[HttpPut("{groupId:int}/post/{postId:int}/comment")]
		public IActionResult AddComment(int groupId, int postId, CommentCreationModel creationModel)
		{
			CommentModel model = this.groupService.AddComment(postId, creationModel);
			if (model == null)
			{
				BadRequest();
			}

			return Ok(model);
		}
	}
}
