using System.Security.Claims;
using ResourcesServiceDevelopment.Accessors;

namespace ResourcesServiceDevelopment.Transactions
{
	public class VaultTransactionProvider : IVaultTransactionProvider
	{
		private readonly IBaseUrlAccessor baseUrlAccessor;
		private readonly IWebHostEnvironment webHostEnvironment;

		public VaultTransactionProvider(IBaseUrlAccessor baseUrlAccessor, IWebHostEnvironment webHostEnvironment)
		{
			this.baseUrlAccessor = baseUrlAccessor;
			this.webHostEnvironment = webHostEnvironment;
		}

		public VaultTransaction Open(string userId)
		{
			return new VaultTransaction(baseUrlAccessor.BaseUrl, webHostEnvironment.ContentRootPath, userId);
		}
	}
}
