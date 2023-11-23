using ForumServiceDevelopment.Models.Posts;
using ForumServiceDevelopment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumServiceDevelopment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SearchController : ControllerBase
	{
		private readonly IGroupService groupService;

		public SearchController(IGroupService groupService)
		{
			this.groupService = groupService;
		}

		[Authorize]
		[HttpGet("{text}")]
		public IActionResult GetSearchResults(string text)
		{
			List<PostSimpleModel> models = this.groupService.GetPosts(text);

			return Ok(models);
		}
	}
}
