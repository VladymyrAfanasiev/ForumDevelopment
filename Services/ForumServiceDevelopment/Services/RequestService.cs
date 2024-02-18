using ForumServiceDevelopment.Data;
using ForumServiceDevelopment.Data.Models;
using ForumServiceDevelopment.Models.Requests;

namespace ForumServiceDevelopment.Services
{
	public class RequestService : IRequestService
	{
		private readonly ForumDatabaseContext databaseContext;

		public RequestService(ForumDatabaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public List<RequestGroupModel> GetGroupRequests()
		{
			return this.databaseContext.GroupRequests
				.Select(r => new RequestGroupModel(r))
				.ToList();
		}

		public List<RequestGroupModel> GetGroupRequests(Guid authorId)
		{
			return this.databaseContext.GroupRequests
				.Where(gr => gr.AuthorId == authorId)
				.Select(gr => new RequestGroupModel(gr))
				.ToList();
		}

		public RequestGroupModel AddGroupRequest(RequestAddGroupModel model, Guid authorId)
		{
			GroupRequest groupRequest = model.ToGroupRequestEntity();
			groupRequest.AuthorId = authorId;

			this.databaseContext.GroupRequests.Add(groupRequest);
			this.databaseContext.SaveChanges();

			return new RequestGroupModel(groupRequest);
		}

		public RequestGroupModel ChangeRequestState(Guid requestId, RequestStatusEnum newStatus)
		{
			GroupRequest groupRequest = this.databaseContext.GroupRequests.FirstOrDefault(gr => gr.Id == requestId);
			if (groupRequest == null)
			{
				return null;
			}

			if (groupRequest.Status == newStatus)
			{
				return null;
			}

			if (groupRequest.Status != RequestStatusEnum.New)
			{
				return null;
			}

			if (newStatus == RequestStatusEnum.Approved)
			{
				Group existedGroup = this.databaseContext.Groups.FirstOrDefault(gr => gr.Name == groupRequest.Name);
				if (existedGroup != null)
				{
					return null;
				}

				groupRequest.Status = RequestStatusEnum.Approved;

				RequestGroupModel requestModel = new RequestGroupModel(groupRequest);
				Group newGroup = requestModel.ToGroupEntity();

				this.databaseContext.Groups.Add(newGroup);
			}
			else if (newStatus == RequestStatusEnum.Declined)
			{
				groupRequest.Status = RequestStatusEnum.Declined;
			}
			else
			{
				throw new NotSupportedException();
			}

			this.databaseContext.SaveChanges();

			return new RequestGroupModel(groupRequest);
		}
	}
}
