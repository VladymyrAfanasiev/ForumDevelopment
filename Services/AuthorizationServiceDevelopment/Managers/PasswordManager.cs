using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AuthorizationServiceDevelopment.Managers
{
	// TODO: Investigate PasswordHasher
	public class PasswordManager : IPasswordManager
	{
		public string GeneratePasswordHash(string password, out string salt)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			byte[] saltBytes = GenerateSalt();

			string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password!,
				salt: saltBytes,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));

			salt = Convert.ToBase64String(saltBytes);
			return hashedPassword;
		}

		private byte[] GenerateSalt()
		{
			return RandomNumberGenerator.GetBytes(128 / 8);
		}
	}
}
