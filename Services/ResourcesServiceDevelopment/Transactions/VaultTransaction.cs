using System.Security.Claims;

namespace ResourcesServiceDevelopment.Transactions
{
	public class VaultTransaction : IDisposable
	{
		private const string UserImage = "UserImage";

		private readonly string baseUrl;
		private readonly string rootPath;
		private readonly string userId;

		private string publicFolderPath = string.Empty;

		public VaultTransaction(string baseUrl, string rootPath, string userId)
		{
			this.baseUrl = baseUrl.TrimEnd('/');
			this.rootPath = rootPath;
			this.userId = userId;
		}

		private string PublicFolderPath
		{
			get
			{
				if (string.IsNullOrEmpty(publicFolderPath))
				{
					publicFolderPath = Path.Combine(rootPath, "Vault", "Public", userId);
					if (!Directory.Exists(publicFolderPath))
					{
						Directory.CreateDirectory(publicFolderPath);
					}
				}

				return publicFolderPath;
			}
		}

		private string PrivateFolderPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void SaveOrUpdateUserImage(IFormFile imageFile)
		{
			string[] userImages = Directory.GetFiles(PublicFolderPath, $"{UserImage}.*");
			foreach (string image in userImages)
			{
				File.Delete(image);
			}

			string imagePath = Path.Combine(PublicFolderPath, $"{UserImage}{Path.GetExtension(imageFile.FileName)}");
			using (FileStream stream = new FileStream(imagePath, FileMode.Create))
			{
				imageFile.CopyTo(stream);
			}
		}

		public string GetUserImageUrl()
		{
			string[] userImages = Directory.GetFiles(PublicFolderPath, $"{UserImage}.*");
			if (userImages.Length == 0)
			{
				return string.Empty;
			}

			return $"{baseUrl}/Public/{userId}/{Path.GetFileName(userImages[0])}";
		}

		public void Dispose()
		{

		}
	}
}
