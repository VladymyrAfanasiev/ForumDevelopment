using AuthorizationServiceDevelopment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServiceDevelopment.Data
{
	public class AuthorizationDbContext : DbContext
	{
		public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
			: base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<User> Users { get; set; }
	}
}
