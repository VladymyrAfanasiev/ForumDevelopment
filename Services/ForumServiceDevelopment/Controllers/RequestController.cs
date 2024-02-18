using ForumServiceDevelopment.Data.Models;
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
	public class RequestController : ControllerBase
	{
		private readonly IRequestService requestService;

		public RequestController(IRequestService requestService)
		{
			this.requestService = requestService;
		}

		[Authorize]
		[RoleRequirementFilter(UserRole.Admin)]
		[HttpGet]
		public IActionResult GetRequests()
		{
			List<RequestGroupModel> models = this.requestService.GetGroupRequests();

			return Ok(models);
		}

		[Authorize]
		[HttpGet("userrequests")]
		public IActionResult GetUserRequests()
		{
			UserInfo userInfo = User.GetAuthorizedUserInfo();
			List<RequestGroupModel> models = this.requestService.GetGroupRequests(userInfo.Id);

			return Ok(models);
		}

		[Authorize]
		[HttpPut]
		public IActionResult RequestAddGroup(RequestAddGroupModel requestAddGroupModel)
		{
			UserInfo userInfo = User.GetAuthorizedUserInfo();
			RequestGroupModel model = this.requestService.AddGroupRequest(requestAddGroupModel, userInfo.Id);
			if (model == null)
			{
				return BadRequest();
			}

			if (userInfo.IsAdmin)
			{
				this.requestService.ChangeRequestState(model.Id, RequestStatusEnum.Approved);
			}

			return Ok(model);
		}

		[Authorize]
		[RoleRequirementFilter(UserRole.Admin)]
		[HttpPost("{requestId:guid}")]
		public IActionResult ProcessRequest(Guid requestId, ProcessRequestModel processRequestModel)
		{
			if (!Enum.TryParse(processRequestModel.newStatus, out RequestStatusEnum newRequestStatusEnum))
			{
				return BadRequest();
			}

			RequestGroupModel model = this.requestService.ChangeRequestState(requestId, newRequestStatusEnum);
			if (model == null)
			{
				return BadRequest();
			}

			return Ok(model);
		}
	}
}
