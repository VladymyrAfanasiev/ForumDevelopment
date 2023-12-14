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

			modelBuilder.Entity<GroupRequest>();

			modelBuilder.Entity<Group>()
				.HasMany(g => g.Posts)
				.WithOne(gi => gi.Group)
				.HasForeignKey(gi => gi.GroupId);

			modelBuilder.Entity<Post>()
				.HasMany(gi => gi.Comments)
				.WithOne(c => c.Post)
				.HasForeignKey(c => c.PostId);

			modelBuilder.Entity<Comment>()
				.HasMany(c => c.CommentReactions)
				.WithOne(cl => cl.Comment)
				.HasForeignKey(cl => cl.CommentId);
		}

		public DbSet<GroupRequest> GroupRequests { get; set; }

		public DbSet<Group> Groups { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<CommentReaction> CommentReactions { get; set; }
	}
}
