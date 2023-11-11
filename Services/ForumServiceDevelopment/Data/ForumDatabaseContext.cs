using ForumServiceDevelopment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumServiceDevelopment.Data
{
	public class ForumDatabaseContext : DbContext
	{
		public ForumDatabaseContext(DbContextOptions<ForumDatabaseContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Posts)
				.WithOne(gi => gi.Group)
				.HasForeignKey(gi => gi.GroupId);

			modelBuilder.Entity<Post>()
				.HasMany(gi => gi.Comments)
				.WithOne(c => c.Post)
				.HasForeignKey(c => c.PostId);
		}


		public DbSet<Group> Groups { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }
	}
}
