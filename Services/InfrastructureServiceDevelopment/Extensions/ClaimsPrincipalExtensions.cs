using System.Security.Claims;
using InfrastructureServiceDevelopment.Authentication;

namespace InfrastructureServiceDevelopment.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static UserInfo GetAuthorizedUserInfo(this ClaimsPrincipal user)
		{
			string claimId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			string claimRole = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
			if (!string.IsNullOrEmpty(claimId) && Guid.TryParse(claimId, out Guid id) &&
				!string.IsNullOrEmpty(claimRole) && UserRole.TryParse(claimRole, out UserRole role))
			{
				return new UserInfo(id, role);
			}

			return new UserInfo();
		}
	}
}
