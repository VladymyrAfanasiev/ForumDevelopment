using InfrastructureServiceDevelopment.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InfrastructureServiceDevelopment.Authentication
{
	public class RoleRequirementFilter : Attribute, IAuthorizationFilter
	{
		private readonly UserRole userRole;

		public RoleRequirementFilter(UserRole userRole)
		{
			this.userRole = userRole;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (context.HttpContext.User.GetAuthorizedUserInfo().Role != userRole)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
