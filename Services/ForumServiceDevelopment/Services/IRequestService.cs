using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models.Requests;

namespace ForumServiceDevelopment.Services
{
	public interface IRequestService
	{
		List<RequestGroupModel> GetGroupRequests();

		List<RequestGroupModel> GetGroupRequests(Guid authorId);

		RequestGroupModel AddGroupRequest(RequestAddGroupModel model, Guid authorId);

		RequestGroupModel ChangeRequestState(Guid requestId, RequestStatusEnum newStatus);
	}
}
