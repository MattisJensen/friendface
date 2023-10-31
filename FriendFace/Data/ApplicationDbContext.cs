using FriendFace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FriendFace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // DbSet for each entity/table we want to interact with
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollowsUser> UserFollowsUsers { get; set; }
        public DbSet<UserLikesPost> UserLikesPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to be called before the custom model configurations

            /*   // User to Post relation: One-to-Many
               modelBuilder.Entity<User>()
                   .HasMany(u => u.Posts)
                   .WithRequired(p => p.User)
                   .HasForeignKey(p => p.UserId);

               // User to Comment relation: One-to-Many
               modelBuilder.Entity<User>()
                   .HasMany(u => u.Comments)
                   .WithRequired(c => c.User)
                   .HasForeignKey(c => c.UserId);

               // Post to Comment relation: One-to-Many
               modelBuilder.Entity<Post>()
                   .HasMany(p => p.Comments)
                   .WithRequired(c => c.Post)
                   .HasForeignKey(c => c.PostId);

               // Follower-Following relation: Many-to-Many
               modelBuilder.Entity<User>()
                   .HasMany(u => u.Following)
                   .WithMany(u => u.Followers)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserId");
                       cs.MapRightKey("FollowerId");
                       cs.ToTable("UserFollowers");
                   });
           }*/
        }
    }
}