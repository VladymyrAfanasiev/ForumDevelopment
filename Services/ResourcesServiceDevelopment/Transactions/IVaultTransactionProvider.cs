using System.Security.Claims;

namespace ResourcesServiceDevelopment.Transactions
{
	public interface IVaultTransactionProvider
	{
		VaultTransaction Open(string userId);
	}
}
