using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcesServiceDevelopment.Transactions;

namespace ResourcesServiceDevelopment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublicVaultController : ControllerBase
	{
		private readonly IVaultTransactionProvider vaultTransactionProvider;

		public PublicVaultController(IVaultTransactionProvider vaultTransactionProvider)
		{
			this.vaultTransactionProvider = vaultTransactionProvider;
		}

		[HttpGet("userimage/{userId:guid}")]
		public IActionResult Get(Guid userId)
		{
			string userImageUrl = string.Empty;

			using (VaultTransaction transaction = vaultTransactionProvider.Open(userId.ToString()))
			{
				userImageUrl = transaction.GetUserImageUrl();
			}

			if (string.IsNullOrEmpty(userImageUrl))
			{
				return NotFound();
			}

			return Ok(userImageUrl);
		}

		[Authorize]
		[HttpPost("userimage")]
		public IActionResult Upload([FromForm(Name = "UserImage")] IFormFile imageFile)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			using (VaultTransaction transaction = vaultTransactionProvider.Open(userId))
			{
				transaction.SaveOrUpdateUserImage(imageFile);
			}

			return Ok();
		}

		[Authorize]
		[HttpDelete("userimage")]
		public IActionResult Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
